using Core.Entites;
using Core.ResponseSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IUserActivityServices
    {
        public Task<ApiResponse<List<Book>>> GetFavouriteList();
        public Task<ApiResponse<List<Book>>> GetReadList();
        public Task<ApiResponse<List<Book>>> GetToReadList();
        public Task<ApiResponse<List<Book>>> GetCurrentlyReadingList();
        public Task<ApiResponse<string>> AddBookToFavouriteList(int BookId);
        public Task<ApiResponse<string>> AddBookToToReadList(int BookId);
        public Task<ApiResponse<string>> AddBookToReadListList(int BookId);
        public Task<ApiResponse<string>> AddBookToCurrentlyReadingList(int BookId);
        public Task<ApiResponse<string>> AddNoteToBook(Note note);
        public Task<ApiResponse<string>> RemoveNoteToBook(int NoteId);
        public Task<ApiResponse<string>> RemoveBookFromFavouriteList(int BookId);
        public Task<ApiResponse<string>> RemoveBookFromToReadList(int BookId);
        public Task<ApiResponse<string>> RemoveBookFromReadList(int BookId);
        public Task<ApiResponse<string>> RemoveBookFromCurrentlyReadingList(int BookId);
    }
}
