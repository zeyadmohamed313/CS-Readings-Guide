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
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookServices : ResponseHandler,  IBookServices
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public BookServices(IUnitOfWork unitOfWork,
            IDistributedCache distributedCache,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _distributedCache = distributedCache;
            _mapper = mapper;
        }
        #endregion
        public async Task<ApiResponse<string>> AddBook(BookDtoWithOutId book)
        {
            // Checking for the book 
            if(book == null)
                return BadRequest<string>("Insertion Failds");
            // Mapping 
            var mapping = _mapper.Map<Book>(book);   
            // Adding The Book 
            await _unitOfWork.bookRepository.Add(mapping);

            _unitOfWork.commit();

            return Success("Insertion Success");
        }

        public async Task<ApiResponse<string>> DeleteBook(int BookId)
        {
            // Checking for the book 
            var book = await _unitOfWork.bookRepository.GetAsync(BookId);

            // Check For Value 
            if (book == null)
                return BadRequest<string>("This Book Is Not Found");

            await _unitOfWork.bookRepository.DeleteAsync(BookId);
            _unitOfWork.commit();

            return Success<string>("Deletion Done Successfully");
        }

        public async Task<ApiResponse<List<BookDto>>> GetAllBook()
        {
            var cacheKey = "GetAllBook";
            var cachedBooks = await _distributedCache.GetStringAsync(cacheKey);
            if (cachedBooks != null)
            {
                // Books found in cache, deserialize and return
                var books = JsonConvert.DeserializeObject<List<Book>>(cachedBooks);
                var mapping = _mapper.Map<List<BookDto>>(books);
                return Success(mapping);
            }
            else
            {
                // Books not found in cache, retrieve from database
                var books = await _unitOfWork.bookRepository.GetAllAsync();

                if (books == null)
                    return BadRequest<List<BookDto>>("There Is No Books To Show");

                // Cache the books
                var serializedBooks = JsonConvert.SerializeObject(books);
                await _distributedCache.SetStringAsync(cacheKey, serializedBooks);
                var mapping = _mapper.Map<List<BookDto>>(books);
                return Success(mapping);
            }

        }

        public async Task<ApiResponse<BookDto>> GetBookById(int Id)
        {
            // Try To Get The Book 
            var book = await _unitOfWork.bookRepository.GetAsync(Id);

            // Check Value 
            if (book == null)
                return BadRequest<BookDto>("Book Is Not Found");
            // Mapping
            var mapping = _mapper.Map<BookDto>(book);

            return Success(mapping);

        }

        public async Task<ApiResponse<List<BookDto>>> GetBookWithOneCategory(int CategoryId)
        {
            var cacheKey = $"CategoryBooks:{CategoryId}";
            var cachedBooks = await _distributedCache.GetStringAsync(cacheKey);

            if (cachedBooks != null)
            {
                // Books found in cache, deserialize and return
                var books = JsonConvert.DeserializeObject<List<Book>>(cachedBooks);
                var mapping = _mapper.Map<List<BookDto>>(books);
                return Success(mapping);
            }
            else
            {
                // Books not found in cache, retrieve from database
                var books = await _unitOfWork.bookRepository.GetBooksWithCategory(CategoryId);

                if (books == null || books.Count == 0)
                {
                    // No books found in this category
                    return BadRequest<List<BookDto>>("There Are No Books In This Category");
                }

                // Cache the books
                var serializedBooks = JsonConvert.SerializeObject(books);
                await _distributedCache.SetStringAsync(cacheKey, serializedBooks);
                var mapping = _mapper.Map<List<BookDto>>(books);
                return Success(mapping);
            }

        }

        public async Task<ApiResponse<string>> UpdateBook(BookDto book)
        {
            if(book == null)
                return BadRequest<string>("The Given Book Is Empty");
            // Mapping 
            var mapping = _mapper.Map<Book>(book);
            await _unitOfWork.bookRepository.Update(mapping);
            _unitOfWork.commit();
            return Success("Updated Successfully");
        }
    }
}
