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
    public interface IUserActivityServices
    {
        public Task<ApiResponse<List<BookDto>>> GetFavouriteList();
        public Task<ApiResponse<List<BookDto>>> GetReadList();
        public Task<ApiResponse<List<BookDto>>> GetToReadList();
        public Task<ApiResponse<List<BookDto>>> GetCurrentlyReadingList();
        public Task<ApiResponse<List<NoteDto>>> GetNotes(int BookId);

        public Task<ApiResponse<string>> AddBookToFavouriteList(int BookId);
        public Task<ApiResponse<string>> AddBookToToReadList(int BookId);
        public Task<ApiResponse<string>> AddBookToReadListList(int BookId);
        public Task<ApiResponse<string>> AddBookToCurrentlyReadingList(int BookId);
        public Task<ApiResponse<string>> AddNoteToBook(NoteDto note);
        public Task<ApiResponse<string>> RemoveNoteToBook(int BookId,int NoteId);
        public Task<ApiResponse<string>> RemoveBookFromFavouriteList(int BookId);
        public Task<ApiResponse<string>> RemoveBookFromToReadList(int BookId);
        public Task<ApiResponse<string>> RemoveBookFromReadList(int BookId);
        public Task<ApiResponse<string>> RemoveBookFromCurrentlyReadingList(int BookId);
    }
}
