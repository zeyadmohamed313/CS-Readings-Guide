using AutoMapper;
using Core;
using Core.Dtos;
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
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public CategoryServices(IUnitOfWork unitOfWork,
            IDistributedCache distributedCache,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _distributedCache = distributedCache;
            _mapper = mapper;
        }
        #endregion
        #region HandleFunctions 
        #endregion

        public async Task<ApiResponse<CategoryDto>> GetCategoryById(int Id)
        {
            // Try To Get The Book 
            var book = await _unitOfWork.categoryRepository.GetAsync(Id);

            // Check Value 
            if (book == null)
                return BadRequest<CategoryDto>("Book Is Not Found");
            // Mapping
            var mapping = _mapper.Map<CategoryDto>(book);

            return Success(mapping);

        }
        public async Task<ApiResponse<string>> AddCategory(CategoryDtoWithOutId category)
        {
            // Checking for the book 
            if (category == null)
                return BadRequest<string>("Insertion Failds");

            //mapping 
            var mapping = _mapper.Map<Category>(category);

            // Adding The Book 
            await _unitOfWork.categoryRepository.Add(mapping);
            _unitOfWork.commit();
            return Success("Insertion Success");
        }

        public async Task<ApiResponse<string>> DeleteCategory(int CategoryId)
        {
            // Checking for the book 
            var category = await _unitOfWork.categoryRepository.GetAsync(CategoryId);

            // Check For Value 
            if (category == null)
                return BadRequest<string>("This Category Is Not Found");

            await _unitOfWork.categoryRepository.DeleteAsync(CategoryId);
            _unitOfWork.commit();

            return Success<string>("Deletion Done Successfully");
        }

        public async Task<ApiResponse<List<CategoryDto>>> GetAllCategories()
        {
            var cacheKey = "GetAllCategories";
            var cachedCategories = await _distributedCache.GetStringAsync(cacheKey);
            var waitfornow = true;
            if (cachedCategories != null)
            {
                var categories = JsonConvert.DeserializeObject<List<Category>>(cachedCategories);
                var mapping = _mapper.Map<List<CategoryDto>>(categories);

                return Success(mapping);
            }
            else
            {
                // Books not found in cache, retrieve from database
                var Categories = await _unitOfWork.categoryRepository.GetAllAsync();

                if (Categories == null)
                    return BadRequest<List<CategoryDto>>("There Is No Categories To Show");

                var mapping = _mapper.Map<List<CategoryDto>>(Categories);

                // Cache the books
                var serializedCtg = JsonConvert.SerializeObject(Categories);
                await _distributedCache.SetStringAsync(cacheKey, serializedCtg);

                return Success(mapping);
            }
        }

        public async Task<ApiResponse<string>> UpdateCategory(CategoryDto category)
        {
            if (category == null)
                return BadRequest<string>("The Given category Is Empty");
            var mapping = _mapper.Map<Category>(category);

            await _unitOfWork.categoryRepository.Update(mapping);
            _unitOfWork.commit();
            return Success("Updated Successfully");
        }

        
    }
}
