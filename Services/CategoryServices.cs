using Core;
using Core.Entites;
using Core.ResponseSchema;
using Core.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryServices :ResponseHandler, ICategoryServices
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _distributedCache;
        #endregion
        #region Constructor
        public CategoryServices(IUnitOfWork unitOfWork, IDistributedCache distributedCache)
        {
            _unitOfWork = unitOfWork;
            _distributedCache = distributedCache;
        }
        #endregion
        #region HandleFunctions 
        #endregion
        public async Task<ApiResponse<string>> AddCategory(Category category)
        {
            // Checking for the book 
            if (category == null)
                return BadRequest<string>("Insertion Failds");

            // Adding The Book 
            await _unitOfWork.categoryRepository.Add(category);

            return Success("Insertion Success");
        }

        public async Task<ApiResponse<string>> DeleteCategory(int CategoryId)
        {
            // Checking for the book 
            var category = await _unitOfWork.categoryRepository.GetAsync(CategoryId);

            // Check For Value 
            if (category == null)
                return BadRequest<string>("This Category Is Not Found");

            await _unitOfWork.bookRepository.DeleteAsync(CategoryId);

            return Success<string>("Deletion Done Successfully");
        }

        public async Task<ApiResponse<List<Category>>> GetAllCategories()
        {
            var cacheKey = "GetAllCategories";
            var cachedCategories = await _distributedCache.GetStringAsync(cacheKey);

            if (cachedCategories != null)
            {
                // Books found in cache, deserialize and return
                var categories = JsonConvert.DeserializeObject<List<Category>>(cachedCategories);
                return Success(categories);
            }
            else
            {
                // Books not found in cache, retrieve from database
                var Categories = await _unitOfWork.categoryRepository.GetAllAsync();

                if (Categories == null)
                    return BadRequest<List<Category>>("There Is No Categories To Show");

                // Cache the books
                var serializedCtg = JsonConvert.SerializeObject(Categories);
                await _distributedCache.SetStringAsync(cacheKey, serializedCtg);

                return Success(Categories);
            }
        }

        public async Task<ApiResponse<string>> UpdateCategory(Category category)
        {
            if (category == null)
                return BadRequest<string>("The Given category Is Empty");
            await _unitOfWork.categoryRepository.Update(category);
            return Success("Updated Successfully");
        }
    }
}
