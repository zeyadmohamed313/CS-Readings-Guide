using Core.Dtos;
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
    [Authorize]
    public class UserActivityController : AppControllerBase
    {
        #region Fields 
        private readonly IUserActivityServices _userActivityServices;
        #endregion
        #region Constructor
        public UserActivityController(IUserActivityServices userActivityServices)
        {
            _userActivityServices = userActivityServices;
        }
        #endregion
        #region HandleActions
        [HttpGet]
        [Route(RoutingSchema.UserActivityRouting.GetFavouriteList)]
        [SwaggerOperation(Summary = "قائمة الكتب المفضلة", OperationId = "GetFavouriteList")]
        public async Task<IActionResult> GetFavouriteList()
        {
            var result = await _userActivityServices.GetFavouriteList();
            return NewResult(result);
        }
        [HttpGet]
        [Route(RoutingSchema.UserActivityRouting.GetCurrentlyReadingList)]
        [SwaggerOperation(Summary = "قائمة الكتب المقروئة حاليا", OperationId = "GetCurrentlyReadingList")]
        public async Task<IActionResult> GetCurrentlyReadingList()
        {
            var result = await _userActivityServices.GetFavouriteList();
            return NewResult(result);
        }
        [HttpGet]
        [Route(RoutingSchema.UserActivityRouting.GetReadList)]
        [SwaggerOperation(Summary = "قائمة الكتب التي تم قرائتها", OperationId = "GetReadList")]
        public async Task<IActionResult> GetReadList()
        {
            var result = await _userActivityServices.GetReadList();
            return NewResult(result);
        }
        [HttpGet]
        [Route(RoutingSchema.UserActivityRouting.GetToReadList)]
        [SwaggerOperation(Summary = "قائمة الكتب التي انوي قرائتها", OperationId = "GetToReadList")]
        public async Task<IActionResult> GetToReadList()
        {
            var result = await _userActivityServices.GetToReadList();
            return NewResult(result);
        }
        [HttpGet]
        [Route(RoutingSchema.UserActivityRouting.GetNotes)]
        [SwaggerOperation(Summary = "عرض الملاحظات علي هذا الكتاب", OperationId = "GetNotes")]
        public async Task<IActionResult> GetNotes([FromRoute] int BookId)
        {
            var result = await _userActivityServices.GetNotes(BookId);
            return NewResult(result);
        }
        [HttpPost]
        [Route(RoutingSchema.UserActivityRouting.AddBookToFavouriteList)]
        [SwaggerOperation(Summary = "اضافة كتاب الي المفضلة", OperationId = "AddBookToFavouriteList")]
        public async Task<IActionResult> AddBookToFavouriteList([FromRoute]int Id)
        {
            var result = await _userActivityServices.AddBookToFavouriteList(Id);
            return NewResult(result);
        }
        [HttpPost]
        [Route(RoutingSchema.UserActivityRouting.AddBookToCurrentlyReadingList)]
        [SwaggerOperation(Summary = "اضافة كتاب الي المقروئات حاليا", OperationId = "AddBookToCurrentlyReadingList")]
        public async Task<IActionResult> AddBookToCurrentlyReadingList([FromRoute] int Id)
        {
            var result = await _userActivityServices.AddBookToCurrentlyReadingList(Id);
            return NewResult(result);
        }
        [HttpPost]
        [Route(RoutingSchema.UserActivityRouting.AddBookToReadList)]
        [SwaggerOperation(Summary = "اضافة كتاب الي الذي تم قرائته", OperationId = "AddBookToReadList")]
        public async Task<IActionResult> AddBookToReadList([FromRoute] int Id)
        {
            var result = await _userActivityServices.AddBookToReadListList(Id);
            return NewResult(result);
        }
        [HttpPost]
        [Route(RoutingSchema.UserActivityRouting.AddBookToToReadList)]
        [SwaggerOperation(Summary = "اضافة كتاب الي الذي سيتم قرائته في المستقبل", OperationId = "AddBookToToReadList")]
        public async Task<IActionResult> AddBookToToReadList([FromRoute] int Id)
        {
            var result = await _userActivityServices.AddBookToToReadList(Id);
            return NewResult(result);
        }

        [HttpPost]
        [Route(RoutingSchema.UserActivityRouting.AddNote)]
        [SwaggerOperation(Summary = "اضافة ملحوظة لكتاب", OperationId = "AddNote")]
        public async Task<IActionResult> AddNote([FromBody]NoteDto note)
        {
            var result = await _userActivityServices.AddNoteToBook(note);
            return NewResult(result);
        }

        [HttpPut]
        [Route(RoutingSchema.UserActivityRouting.DeleteNote)]
        [SwaggerOperation(Summary = "حذف ملحوظة لكتاب", OperationId = "DeleteNote")]
        public async Task<IActionResult> DeleteNote( int BookId,int NoteId)
        {
            var result = await _userActivityServices.RemoveNoteToBook(BookId,NoteId);
            return NewResult(result);
        }

        [HttpDelete]
        [Route(RoutingSchema.UserActivityRouting.RemoveBookFromFavouriteList)]
        [SwaggerOperation(Summary = "حذف كتاب من المفضلة", OperationId = "RemoveBookFromFavouriteList")]
        public async Task<IActionResult> RemoveBookFromFavouriteList([FromRoute] int Id)
        {
            var result = await _userActivityServices.RemoveBookFromFavouriteList(Id);
            return NewResult(result);
        }
        [HttpDelete]
        [Route(RoutingSchema.UserActivityRouting.RemoveBookFromCurrentlyReadingList)]
        [SwaggerOperation(Summary = "حذف كتاب من القراءة الحالية", OperationId = "RemoveBookFromCurrentlyReadingList")]
        public async Task<IActionResult> RemoveBookFromCurrentlyReadingList([FromRoute] int Id)
        {
            var result = await _userActivityServices.RemoveBookFromCurrentlyReadingList(Id);
            return NewResult(result);
        }
        [HttpDelete]
        [Route(RoutingSchema.UserActivityRouting.RemoveBookFromReadList)]
        [SwaggerOperation(Summary = "حذف كتاب من القراءة الذي تم قراءته", OperationId = "RemoveBookFromReadList")]
        public async Task<IActionResult> RemoveBookFromReadList([FromRoute] int Id)
        {
            var result = await _userActivityServices.RemoveBookFromReadList(Id);
            return NewResult(result);
        }
        [HttpDelete]
        [Route(RoutingSchema.UserActivityRouting.RemoveBookFromToReadList)]
        [SwaggerOperation(Summary = "حذف كتاب من القراءة الذي سيتم قرائته", OperationId = "RemoveBookFromToReadList")]
        public async Task<IActionResult> RemoveBookFromToReadList([FromRoute] int Id)
        {
            var result = await _userActivityServices.RemoveBookFromReadList(Id);
            return NewResult(result);
        }
        #endregion
    }
}
