using Core.Dtos;
using Core.Services;
using CS_Readings_Guide.Base;
using CS_Readings_Guide.Router;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Swashbuckle.AspNetCore.Annotations;

namespace CS_Readings_Guide.Controllers
{
    [ApiController]
    public class CategoryController : AppControllerBase
    {
        #region Fields 
        private readonly ICategoryServices _categoryServices;
        #endregion

        #region Constructor
        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        #endregion
        #region HandleActions 
        [HttpGet]
        [Route(RoutingSchema.CategoryRouting.GetAllBooks)]
        [SwaggerOperation(Summary = "عرض كل التصنيف", OperationId = "GetAllCategories")]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _categoryServices.GetAllCategories();
            return NewResult(result);
        }

        [HttpGet]
        [Route(RoutingSchema.CategoryRouting.GetBookById)]
        [SwaggerOperation(Summary = "عرض احد التصنيفات", OperationId = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int Id)
        {
            var result = await _categoryServices.GetCategoryById(Id);
            return NewResult(result);
        }

        [HttpPost]
        [Route(RoutingSchema.CategoryRouting.AddBook)]
        [SwaggerOperation(Summary = "اضافة تصنيف جديد", OperationId = "AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryDtoWithOutId category)
        {
            var result = await _categoryServices.AddCategory(category);
            return NewResult(result);
        }
        [HttpPut]
        [Route(RoutingSchema.CategoryRouting.UpdateBook)]
        [SwaggerOperation(Summary = "تعديل تصنيف ", OperationId = "UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryDto category)
        {
            var result = await _categoryServices.UpdateCategory(category);
            return NewResult(result);
        }
        [HttpDelete]
        [Route(RoutingSchema.CategoryRouting.Delete)]
        [SwaggerOperation(Summary = "حذف تصنيف ", OperationId = "DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int Id)
        {
            var result = await _categoryServices.DeleteCategory(Id);
            return NewResult(result);
        }

        #endregion
    }
}
