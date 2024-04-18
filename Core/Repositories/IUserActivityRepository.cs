using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUserActivityRepository
    {
        public Task<List<Book>> GetFavouriteList(string UserId);
        public Task<List<Book>> GetReadList(string UserId);
        public Task<List<Book>> GetToReadList(string UserId);
        public Task<List<Book>> GetCurrentlyReadingList(string UserId);
        public Task<string> AddBookToFavouriteList(string UserId,int BookId);
        public Task<string> AddBookToToReadList(string UserId, int BookId);
        public Task<string> AddBookToReadListList(string UserId, int BookId);
        public Task<string> AddBookToCurrentlyReadingList(string UserId, int BookId);
        public Task<string> AddNoteToBook(string UserId, Note note); 
        public Task<string> RemoveNoteToBook(string UserId, int NoteId);
        public Task<string> RemoveBookFromFavouriteList(string UserId,int BookId);
        public Task<string> RemoveBookFromToReadList(string UserId, int BookId);
        public Task<string> RemoveBookFromReadList(string UserId, int BookId);
        public Task<string> RemoveBookFromCurrentlyReadingList(string UserId, int BookId);
    }
}
