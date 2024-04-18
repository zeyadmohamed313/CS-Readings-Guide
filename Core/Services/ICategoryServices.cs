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
        public Task<ApiResponse<List<Category>>> GetAllCategories();

        public Task<ApiResponse<string>> AddCategory(Category category);
        public Task<ApiResponse<string>> UpdateCategory(Category category);
        public Task<ApiResponse<string>> DeleteCategory(int CategoryId);
    }
}
