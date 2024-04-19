using Core.Dtos;
using Core.Entites;
using Core.ResponseSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ICategoryServices
    {
        public Task<ApiResponse<List<CategoryDto>>> GetAllCategories();

        public Task<ApiResponse<string>> AddCategory(CategoryDtoWithOutId category);
        public Task<ApiResponse<string>> UpdateCategory(CategoryDto category);
        public Task<ApiResponse<CategoryDto>> GetCategoryById(int Id);

        public Task<ApiResponse<string>> DeleteCategory(int CategoryId);
    }
}
