using AutoMapper;
using Core;
using Core.Dtos;
using Core.Entites;
using Core.ResponseSchema;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserActivityServices : ResponseHandler, IUserActivityServices
    {

        #region Fields 
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public UserActivityServices(IHttpContextAccessor httpContextAccessor ,
            IUnitOfWork unitOfWork, IDistributedCache cache,IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _cache = cache;
            _mapper = mapper;
        }
        #endregion

        #region HandleFunctions 
        public async Task<ApiResponse<string>> AddBookToCurrentlyReadingList(int BookId)
        {
            // Get The Current UserId
            var userid = GetAuthenticatedUser();

            // Try To Add book to the List
            var result = await _unitOfWork.userActivityRepository.AddBookToCurrentlyReadingList(userid, BookId);

            if (result != "Success")
                return BadRequest<string>("Insertion Faild !");

            return Success("Insertion Success");

        }

        public async Task<ApiResponse<string>> AddBookToFavouriteList(int BookId)
        {
            // Get The Current UserId
            var userid = GetAuthenticatedUser();

            // Try To Add book to the List
            var result = await _unitOfWork.userActivityRepository.AddBookToFavouriteList(userid, BookId);
            _unitOfWork.commit();
            if (result != "Success")
                return BadRequest<string>(result);

            return Success("Insertion Success");
        }

        public async Task<ApiResponse<string>> AddBookToReadListList(int BookId)
        {
            // Get The Current UserId
            var userid = GetAuthenticatedUser();

            // Try To Add book to the List
            var result = await _unitOfWork.userActivityRepository.AddBookToReadListList(userid, BookId);

            if (result != "Success")
                return BadRequest<string>("Insertion Faild !");

            return Success("Insertion Success");
        }

        public async Task<ApiResponse<string>> AddBookToToReadList(int BookId)
        {
            // Get The Current UserId
            var userid = GetAuthenticatedUser();

            // Try To Add book to the List
            var result = await _unitOfWork.userActivityRepository.AddBookToToReadList(userid, BookId);

            if (result != "Success")
                return BadRequest<string>("Insertion Faild !");

            return Success("Insertion Success");
        }

        public async Task<ApiResponse<string>> AddNoteToBook(NoteDto note)
        {
            // Get The Current UserId
            var userid = GetAuthenticatedUser();

            // Check For Note Health 
            if (note == null)
                return BadRequest<string>("Cannot Insert Empty Note");

            var mapping = _mapper.Map<Note>(note);

            // Try To Add Note
            var result = await _unitOfWork.userActivityRepository.AddNoteToBook(userid,mapping);

            if (result != "Success")
                return BadRequest<string>("Falied To Insert Note");

            return Success("Insertion Success");

        }

        public async Task<ApiResponse<List<BookDto>>> GetCurrentlyReadingList()
        {
            // Get The Current UserId
            var userId = GetAuthenticatedUser();

            // Check cache for currently reading list based on user ID
            var cacheKey = $"CurrentlyReadingList:{userId}";
            var cachedList = await _cache.GetStringAsync(cacheKey);
            if (cachedList != null)
            {
                // Currently reading list found in cache, deserialize and return
                var currentlyReadingList = JsonConvert.DeserializeObject<List<BookDto>>(cachedList);
                return Success(currentlyReadingList);
            }
            else
            {
                // Currently reading list not found in cache, retrieve from repository
                var result = await _unitOfWork.userActivityRepository.GetCurrentlyReadingList(userId);

                if (result == null)
                {
                    // Currently reading list not found for the user
                    return BadRequest<List<BookDto>>("This List Is Not Found");
                }

                // Cache the currently reading list
                var serializedList = JsonConvert.SerializeObject(result);
                await _cache.SetStringAsync(cacheKey, serializedList);

                return Success(result);
            }
        }

        public async Task<ApiResponse<List<BookDto>>> GetFavouriteList()
        {
            // Get The Current UserId
            var userId = GetAuthenticatedUser();

            // Check cache for currently reading list based on user ID
            var cacheKey = $"FavouriteList:{userId}";
            var cachedList = await _cache.GetStringAsync(cacheKey);
            if (cachedList != null)
            {
                // Currently reading list found in cache, deserialize and return
                var FavouriteList = JsonConvert.DeserializeObject<List<BookDto>>(cachedList);
                return Success(FavouriteList);
            }
            else
            {
                // Currently reading list not found in cache, retrieve from repository
                var result = await _unitOfWork.userActivityRepository.GetFavouriteList(userId);

                if (result == null)
                {
                    // Currently reading list not found for the user
                    return BadRequest<List<BookDto>>("This List Is Not Found");
                }

                // Cache the currently reading list
                var serializedList = JsonConvert.SerializeObject(result);
                await _cache.SetStringAsync(cacheKey, serializedList);

                return Success(result);
            }
        }

        public async Task<ApiResponse<List<NoteDto>>> GetNotes(int BookId)
        {
            //Get The Current User ID 
            var userid = GetAuthenticatedUser();
            var Notes = await _unitOfWork.userActivityRepository.GetAllNotes(userid,BookId);
            if (Notes==null)
                return BadRequest<List<NoteDto>>("There Is No Notes Here");

            return Success(Notes);
        }

        public async Task<ApiResponse<List<BookDto>>> GetReadList()
        {
            // Get The Current UserId
            var userId = GetAuthenticatedUser();

            // Check cache for currently reading list based on user ID
            var cacheKey = $"ReadList:{userId}";
            var cachedList = await _cache.GetStringAsync(cacheKey);
            if (cachedList != null)
            {
                // Currently reading list found in cache, deserialize and return
                var ReadList = JsonConvert.DeserializeObject<List<BookDto>>(cachedList);
                return Success(ReadList);
            }
            else
            {
                // Currently reading list not found in cache, retrieve from repository
                var result = await _unitOfWork.userActivityRepository.GetReadList(userId);

                if (result == null)
                {
                    // Currently reading list not found for the user
                    return BadRequest<List<BookDto>>("This List Is Not Found");
                }

                // Cache the currently reading list
                var serializedList = JsonConvert.SerializeObject(result);
                await _cache.SetStringAsync(cacheKey, serializedList);

                return Success(result);
            }
        }

        public async Task<ApiResponse<List<BookDto>>> GetToReadList()
        {
            // Get The Current UserId
            var userId = GetAuthenticatedUser();

            // Check cache for currently reading list based on user ID
            var cacheKey = $"ToReadList:{userId}";
            var cachedList = await _cache.GetStringAsync(cacheKey);
            if (cachedList != null)
            {
                // Currently reading list found in cache, deserialize and return
                var ToReadList = JsonConvert.DeserializeObject<List<BookDto>>(cachedList);
                return Success(ToReadList);
            }
            else
            {
                // Currently reading list not found in cache, retrieve from repository
                var result = await _unitOfWork.userActivityRepository.GetToReadList(userId);

                if (result == null)
                {
                    // Currently reading list not found for the user
                    return BadRequest<List<BookDto>>("This List Is Not Found");
                }

                // Cache the currently reading list
                var serializedList = JsonConvert.SerializeObject(result);
                await _cache.SetStringAsync(cacheKey, serializedList);

                return Success(result);
            }
        }

        public async Task<ApiResponse<string>> RemoveBookFromCurrentlyReadingList(int BookId)
        {
            // Get The Current UserId
            var userid = GetAuthenticatedUser();

            // Try To Add book to the List
            var result = await _unitOfWork.userActivityRepository.RemoveBookFromCurrentlyReadingList(userid, BookId);

            if (result != "Success")
                return BadRequest<string>("Insertion Faild !");

            return Success("Deletion Success");
        }

        public async Task<ApiResponse<string>> RemoveBookFromFavouriteList(int BookId)
        {
            // Get The Current UserId
            var userid = GetAuthenticatedUser();

            // Try To Add book to the List
            var result = await _unitOfWork.userActivityRepository.RemoveBookFromFavouriteList(userid, BookId);

            if (result != "Success")
                return BadRequest<string>("Insertion Faild !");

            return Success("Deletion Success");
        }

        public async Task<ApiResponse<string>> RemoveBookFromReadList(int BookId)
        {
            // Get The Current UserId
            var userid = GetAuthenticatedUser();

            // Try To Add book to the List
            var result = await _unitOfWork.userActivityRepository.RemoveBookFromReadList(userid, BookId);

            if (result != "Success")
                return BadRequest<string>("Insertion Faild !");

            return Success("Deletion Success");
        }

        public async Task<ApiResponse<string>> RemoveBookFromToReadList(int BookId)
        {
            // Get The Current UserId
            var userid = GetAuthenticatedUser();

            // Try To Add book to the List
            var result = await _unitOfWork.userActivityRepository.RemoveBookFromToReadList(userid, BookId);

            if (result != "Success")
                return BadRequest<string>("Insertion Faild !");

            return Success("Deletion Success");
        }

        public async Task<ApiResponse<string>> RemoveNoteToBook(int BookId,int NoteId)
        {
            // Get The Current UserId
            var userid = GetAuthenticatedUser();

           

            // Try To Add Note
            var result = await _unitOfWork.userActivityRepository.RemoveNoteToBook(userid,BookId, NoteId);

            if (result != "Success")
                return BadRequest<string>("Falied To Insert Note");

            return Success("Deletion Success");
        }

        #endregion


        #region Helper Functions 
        private string GetAuthenticatedUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
        #endregion
    }
}
