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
    public interface IBookServices
    {
        public Task<ApiResponse<List<BookDto>>> GetAllBook();
        public Task<ApiResponse<List<BookDto>>> GetBookWithOneCategory(int CategoryId);
        public Task<ApiResponse<BookDto>> GetBookById(int Id);
        public Task<ApiResponse<string>> AddBook(BookDto book);
        public Task<ApiResponse<string>> UpdateBook(BookDto book);
        public Task<ApiResponse<string>> DeleteBook(int BookId);

    }
}
