using AutoMapper;
using Core.Dtos;
using Core.Entites;
using Core.Services;
using CS_Readings_Guide.Base;
using CS_Readings_Guide.Router;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CS_Readings_Guide.Controllers
{
    [ApiController]
    public class BookController : AppControllerBase
    {
        #region Fields 
        private readonly IBookServices _bookServices;
        private readonly IMapper _mapper; 
        #endregion

        #region Constructor
        public BookController(IBookServices bookServices,IMapper mapper)
        {
            _bookServices = bookServices;
            _mapper = mapper;
        }
        #endregion

        #region Actions 
        [Authorize]
        [HttpGet]
        [Route(RoutingSchema.BookRouting.GetAllBooks)]
        [SwaggerOperation(Summary = "عرض كل الكتب", OperationId = "GetAllBook")]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _bookServices.GetAllBook();
            return NewResult(result);
        }

        [HttpGet]
        [Route(RoutingSchema.BookRouting.GetBookById)]
        [SwaggerOperation(Summary = "عرض احد الكتب", OperationId = "GetBookById")]
        public async Task<IActionResult> GetBookById(int Id)
        {
            var result = await _bookServices.GetBookById(Id);
            return NewResult(result);
        }

        [HttpPost]
        [Route(RoutingSchema.BookRouting.AddBook)]
        [SwaggerOperation(Summary = "اضافة كتاب جديد", OperationId = "AddBook")]
        public async Task<IActionResult> AddBook(BookDtoWithOutId book)
        {
            var result  = await _bookServices.AddBook(book);
            return NewResult(result);
        }
        [HttpPut]
        [Route(RoutingSchema.BookRouting.UpdateBook)]
        [SwaggerOperation(Summary = "تعديل كتاب ", OperationId = "UpdateBook")]
        public async Task<IActionResult> UpdateBook(BookDto book)
        {
            var result = await _bookServices.UpdateBook(book);
            return NewResult(result);
        }
        [HttpDelete]
        [Route(RoutingSchema.BookRouting.Delete)]
        [SwaggerOperation(Summary = "حذف كتاب ", OperationId = "DeleteBook")]
        public async Task<IActionResult> DeleteBook([FromRoute]int Id)
        {
            var result = await _bookServices.DeleteBook(Id);
            return NewResult(result);
        }


        #endregion
    }
}
