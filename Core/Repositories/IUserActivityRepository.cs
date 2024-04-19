using Core.Dtos;
using Core.Entites;
using Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUserActivityRepository
    {
        public Task AddUserLists(AppUser user);
        public Task<List<BookDto>> GetFavouriteList(string UserId);
        public Task<List<BookDto>> GetReadList(string UserId);
        public Task<List<BookDto>> GetToReadList(string UserId);
        public Task<List<BookDto>> GetCurrentlyReadingList(string UserId);
        public Task<List<NoteDto>> GetAllNotes(string UserId,int BookId);
        public Task<string> AddBookToFavouriteList(string UserId,int BookId);
        public Task<string> AddBookToToReadList(string UserId, int BookId);
        public Task<string> AddBookToReadListList(string UserId, int BookId);
        public Task<string> AddBookToCurrentlyReadingList(string UserId, int BookId);
        public Task<string> AddNoteToBook(string UserId, Note note); 
        public Task<string> RemoveNoteToBook(string UserId,int BookId ,int NoteId);
        public Task<string> RemoveBookFromFavouriteList(string UserId,int BookId);
        public Task<string> RemoveBookFromToReadList(string UserId, int BookId);
        public Task<string> RemoveBookFromReadList(string UserId, int BookId);
        public Task<string> RemoveBookFromCurrentlyReadingList(string UserId, int BookId);
    }
}
