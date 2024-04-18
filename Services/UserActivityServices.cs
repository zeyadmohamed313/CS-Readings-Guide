using Core;
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
        #endregion

        #region Constructor
        public UserActivityServices(IHttpContextAccessor httpContextAccessor ,
            IUnitOfWork unitOfWork, IDistributedCache cache)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _cache = cache;
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

            if (result != "Success")
                return BadRequest<string>("Insertion Faild !");

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

        public async Task<ApiResponse<string>> AddNoteToBook(Note note)
        {
            // Get The Current UserId
            var userid = GetAuthenticatedUser();

            // Check For Note Health 
            if (note == null)
                return BadRequest<string>("Cannot Insert Empty Note");

            // Try To Add Note
            var result = await _unitOfWork.userActivityRepository.AddNoteToBook(userid,note);

            if (result != "Success")
                return BadRequest<string>("Falied To Insert Note");

            return Success("Insertion Success");

        }

        public async Task<ApiResponse<List<Book>>> GetCurrentlyReadingList()
        {
            // Get The Current UserId
            var userId = GetAuthenticatedUser();

            // Check cache for currently reading list based on user ID
            var cacheKey = $"CurrentlyReadingList:{userId}";
            var cachedList = await _cache.GetStringAsync(cacheKey);

            if (cachedList != null)
            {
                // Currently reading list found in cache, deserialize and return
                var currentlyReadingList = JsonConvert.DeserializeObject<List<Book>>(cachedList);
                return Success(currentlyReadingList);
            }
            else
            {
                // Currently reading list not found in cache, retrieve from repository
                var result = await _unitOfWork.userActivityRepository.GetCurrentlyReadingList(userId);

                if (result == null)
                {
                    // Currently reading list not found for the user
                    return BadRequest<List<Book>>("This List Is Not Found");
                }

                // Cache the currently reading list
                var serializedList = JsonConvert.SerializeObject(result);
                await _cache.SetStringAsync(cacheKey, serializedList);

                return Success(result);
            }
        }

        public async Task<ApiResponse<List<Book>>> GetFavouriteList()
        {
            // Get The Current UserId
            var userId = GetAuthenticatedUser();

            // Check cache for currently reading list based on user ID
            var cacheKey = $"FavouriteList:{userId}";
            var cachedList = await _cache.GetStringAsync(cacheKey);

            if (cachedList != null)
            {
                // Currently reading list found in cache, deserialize and return
                var FavouriteList = JsonConvert.DeserializeObject<List<Book>>(cachedList);
                return Success(FavouriteList);
            }
            else
            {
                // Currently reading list not found in cache, retrieve from repository
                var result = await _unitOfWork.userActivityRepository.GetFavouriteList(userId);

                if (result == null)
                {
                    // Currently reading list not found for the user
                    return BadRequest<List<Book>>("This List Is Not Found");
                }

                // Cache the currently reading list
                var serializedList = JsonConvert.SerializeObject(result);
                await _cache.SetStringAsync(cacheKey, serializedList);

                return Success(result);
            }
        }

        public async Task<ApiResponse<List<Book>>> GetReadList()
        {
            // Get The Current UserId
            var userId = GetAuthenticatedUser();

            // Check cache for currently reading list based on user ID
            var cacheKey = $"ReadList:{userId}";
            var cachedList = await _cache.GetStringAsync(cacheKey);

            if (cachedList != null)
            {
                // Currently reading list found in cache, deserialize and return
                var ReadList = JsonConvert.DeserializeObject<List<Book>>(cachedList);
                return Success(ReadList);
            }
            else
            {
                // Currently reading list not found in cache, retrieve from repository
                var result = await _unitOfWork.userActivityRepository.GetReadList(userId);

                if (result == null)
                {
                    // Currently reading list not found for the user
                    return BadRequest<List<Book>>("This List Is Not Found");
                }

                // Cache the currently reading list
                var serializedList = JsonConvert.SerializeObject(result);
                await _cache.SetStringAsync(cacheKey, serializedList);

                return Success(result);
            }
        }

        public async Task<ApiResponse<List<Book>>> GetToReadList()
        {
            // Get The Current UserId
            var userId = GetAuthenticatedUser();

            // Check cache for currently reading list based on user ID
            var cacheKey = $"ToReadList:{userId}";
            var cachedList = await _cache.GetStringAsync(cacheKey);

            if (cachedList != null)
            {
                // Currently reading list found in cache, deserialize and return
                var ToReadList = JsonConvert.DeserializeObject<List<Book>>(cachedList);
                return Success(ToReadList);
            }
            else
            {
                // Currently reading list not found in cache, retrieve from repository
                var result = await _unitOfWork.userActivityRepository.GetToReadList(userId);

                if (result == null)
                {
                    // Currently reading list not found for the user
                    return BadRequest<List<Book>>("This List Is Not Found");
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

        public async Task<ApiResponse<string>> RemoveNoteToBook(int NoteId)
        {
            // Get The Current UserId
            var userid = GetAuthenticatedUser();

           

            // Try To Add Note
            var result = await _unitOfWork.userActivityRepository.RemoveNoteToBook(userid, NoteId);

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
