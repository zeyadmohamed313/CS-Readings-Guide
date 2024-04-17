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
        public Task<string> AddBookToFavouriteList(string UserId,int BookId);
        public Task<string> AddBookToToReadList(string UserId, int BookId);
        public Task<string> AddBookToReadListList(string UserId, int BookId);
        public Task<string> AddBookToCurrentlyReadingList(string UserId, int BookId);
        public Task<string> AddNoteToBook(string UserId, int NoteId);
        public Task<string> RemoveNoteToBook(string UserId, int NoteId);
        public Task<string> RemoveBookFromFavouriteList(string UserId,int BookId);
        public Task<string> RemoveBookFromToReadList(string UserId, int BookId);
        public Task<string> RemoveBookFromReadList(string UserId, int BookId);
        public Task<string> RemoveBookFromCurrentlyReadingList(string UserId, int BookId);
    }
}
