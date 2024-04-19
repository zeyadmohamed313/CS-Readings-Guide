using AutoMapper;
using Core.Dtos;
using Core.Entites;
using Core.Entites.Identity;
using Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserActivityRepository : IUserActivityRepository
    {
        #region Fields
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public UserActivityRepository(AppDbContext appDbContext,IMapper mapper)
        {
            _dbContext = appDbContext;
            _mapper = mapper;
        }
        #endregion
        #region HandleFunctions 

        public async Task<string> AddBookToCurrentlyReadingList(string UserId, int BookId)
        {
            // Summon User With Its Favourite List (Eager Loading)
            var User = await _dbContext.Users
                .Include(clr => clr.CurrentlyReadingList)
                .ThenInclude(b => b.Books)
                .FirstOrDefaultAsync(x => x.Id == UserId);

            // Getting The Book With Given Id
            var Book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == BookId);

            // Checking Values
            if (Book == null) return "This Book Is Not Found";
            if (User == null) return "This User Is Not Found";
            if (User.CurrentlyReadingList.Books.Contains(Book))
                return "This Book Is Already Exsists";
            // Adding 
            User.CurrentlyReadingList.Books.Add(Book);

            // Save Changes
            await _dbContext.SaveChangesAsync();

            return "Success";
        }

        public async Task<string> AddBookToFavouriteList(string UserId, int BookId)
        {
            // Summon User With Its Favourite List (Eager Loading)
            var User = await _dbContext.Users
                .Include(clr => clr.FavouriteList)
                .ThenInclude(b => b.Books)
                .FirstOrDefaultAsync(x => x.Id == UserId);

            // Getting The Book With Given Id
            var Book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == BookId);

            // Checking Values
            if (Book == null) return "This Book Is Not Found";
            if (User == null) return "This User Is Not Found";
            if(User.FavouriteList.Books.Contains(Book))
                return "This Book Is Already Exsists";

            // Adding 
            User.FavouriteList.Books.Add(Book);

            // Save Changes
            await _dbContext.SaveChangesAsync();

            return "Success";
        }

        public async Task<string> AddBookToReadListList(string UserId, int BookId)
        {
            // Summon User With Its Read List (Eager Loading)
            var User = await _dbContext.Users
                .Include(clr => clr.ReadList)
                .ThenInclude(b => b.Books)
                .FirstOrDefaultAsync(x => x.Id == UserId);

            // Getting The Book With Given Id
            var Book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == BookId);

            // Checking Values
            if (Book == null) return "This Book Is Not Found";
            if (User == null) return "This User Is Not Found";
            if (User.ReadList.Books.Contains(Book))
                return "This Book Is Already Exsists";
            // Adding 
            User.ReadList.Books.Add(Book);

            // Save Changes
            await _dbContext.SaveChangesAsync();

            return "Success";
        }

        public async Task<string> AddBookToToReadList(string UserId,int BookId)
        {
            // Summon User With Its To Read List (Eager Loading)
            var User = await _dbContext.Users
                .Include(clr => clr.ToReadList)
                .ThenInclude(b => b.Books)
                .FirstOrDefaultAsync(x => x.Id == UserId);

            // Getting The Book With Given Id
            var Book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == BookId);

            // Checking Values
            if (Book == null) return "This Book Is Not Found";
            if (User == null) return "This User Is Not Found";
            if (User.ToReadList.Books.Contains(Book))
                return "This Book Is Already Exsists";
            // Adding 
            User.ToReadList.Books.Add(Book);
            // Save Changes
            await _dbContext.SaveChangesAsync();

            return "Success";
        }

        public async Task<string> AddNoteToBook(string UserId,Note note)
        {
            // Getting the User With The Given ID
            var User = await _dbContext.Users.Include(n=>n.Notes).FirstOrDefaultAsync(i=>i.Id==UserId);
            var Bookk = await _dbContext.Books.Include(n=>n.Notes).FirstOrDefaultAsync(n=>n.Id==note.BookId);

            // Checking Values 
            if (User == null) return "User Is Not Found";
            if (Bookk == null) return "User Is Not Found";
            if (note == null) return "Note Is Not Found";
            note.BookId = note.BookId;
            note.UserId = UserId;
            //Adding 
            User.Notes.Add(note);
            Bookk.Notes.Add(note);
            // Save Changes
            await _dbContext.SaveChangesAsync();

            return "Success";
        }

        public async Task AddUserLists(AppUser user)
        {
            // Get the User 
            var newuser =  await _dbContext.Users
                .Include(f=>f.FavouriteList)
                .Include(clr=>clr.CurrentlyReadingList)
                .Include(rd=>rd.ReadList)
                .Include(trd=>trd.ToReadList)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            // Assign  
            newuser.FavouriteList = new FavouriteList() {AppUserId=user.Id};
            newuser.CurrentlyReadingList = new CurrentlyReadingList() { AppUserId = user.Id };
            newuser.ReadList = new ReadList() { AppUserId = user.Id };
            newuser.ToReadList = new ToReadList() { AppUserId = user.Id };
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<NoteDto>> GetAllNotes(string UserId, int BookId)
        {
            // Get All Note In Common in User And Book
            var Notes = await _dbContext.Notes.Where(u=>u.BookId==BookId&&u.UserId==UserId).ToListAsync();
            var mapping = _mapper.Map<List<NoteDto>>(Notes);  
            return mapping;
        }

        public async Task<List<BookDto>> GetCurrentlyReadingList(string UserId)
        {
            // Getting List for This User
            var Clr = await _dbContext.CurrentlyReadingLists.
                Include(b=>b.Books)
                .FirstOrDefaultAsync(i=>i.AppUserId==UserId);

            var OldBooks = Clr.Books;

            var Books = _mapper.Map<List<BookDto>>(OldBooks);


            return Books;
        }

        public async Task<List<BookDto>> GetFavouriteList(string UserId)
        {
            // Getting List for This User
            var Clr = await _dbContext.FavouriteLists.
                Include(b => b.Books)
                .FirstOrDefaultAsync(i => i.AppUserId == UserId);

            var OldBooks = Clr.Books;

            var Books = _mapper.Map<List<BookDto>>(OldBooks);

           
            return Books;
        }

        public async Task<List<BookDto>> GetReadList(string UserId)
        {
            // Getting List for This User
            var Clr = await _dbContext.ReadLists.
                Include(b => b.Books)
                .FirstOrDefaultAsync(i => i.AppUserId == UserId);

            var OldBooks = Clr.Books;

            var Books = _mapper.Map<List<BookDto>>(OldBooks);


            return Books;
        }

        public async Task<List<BookDto>> GetToReadList(string UserId)
        {
            // Getting List for This User
            var Clr = await _dbContext.ToReadLists.
                Include(b => b.Books)
                .FirstOrDefaultAsync(i => i.AppUserId == UserId);

            var OldBooks = Clr.Books;

            var Books = _mapper.Map<List<BookDto>>(OldBooks);


           return Books;
        }

        public async Task<string> RemoveBookFromCurrentlyReadingList(string UserId, int BookId)
        {
            // Summon User With Its Clr List (Eager Loading)
            var User = await _dbContext.Users
                .Include(clr => clr.CurrentlyReadingList)
                .ThenInclude(b => b.Books)
                .FirstOrDefaultAsync(x => x.Id == UserId);

            // Getting The Book With Given Id
            var Book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == BookId);

            // Checking Values
            if (Book == null) return "This Book Is Not Found";
            if (User == null) return "This User Is Not Found";

            // Check If User Has This Book in the First 
            var IsBookExsists = User.CurrentlyReadingList.Books.Any(i=> i.Id == BookId);
            if (!IsBookExsists) return "Book Doesnot Even Exsists";

            // Adding 
            User.CurrentlyReadingList.Books.Remove(Book);

            // Save Changes
            await _dbContext.SaveChangesAsync();
            return "Success";
        }

        public async Task<string> RemoveBookFromFavouriteList(string UserId, int BookId)
        {
            // Summon User With Its Favourite List (Eager Loading)
            var User = await _dbContext.Users
                .Include(clr => clr.FavouriteList)
                .ThenInclude(b => b.Books)
                .FirstOrDefaultAsync(x => x.Id == UserId);

            // Getting The Book With Given Id
            var Book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == BookId);

            // Checking Values
            if (Book == null) return "This Book Is Not Found";
            if (User == null) return "This User Is Not Found";

            // Check If User Has This Book in the First 
            var IsBookExsists = User.FavouriteList.Books.Any(i => i.Id == BookId);
            if (!IsBookExsists) return "Book Doesnot Even Exsists";

            // Adding 
            User.FavouriteList.Books.Remove(Book);

            // Save Changes
            await _dbContext.SaveChangesAsync();
            return "Success";
        }

        public async Task<string> RemoveBookFromReadList(string UserId, int BookId)
        {
            // Summon User With Its Read List (Eager Loading)
            var User = await _dbContext.Users
                .Include(clr => clr.ReadList)
                .ThenInclude(b => b.Books)
                .FirstOrDefaultAsync(x => x.Id == UserId);

            // Getting The Book With Given Id
            var Book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == BookId);

            // Checking Values
            if (Book == null) return "This Book Is Not Found";
            if (User == null) return "This User Is Not Found";

            // Check If User Has This Book in the First 
            var IsBookExsists = User.ReadList.Books.Any(i => i.Id == BookId);
            if (!IsBookExsists) return "Book Doesnot Even Exsists";

            // Adding 
            User.ReadList.Books.Remove(Book);

            // Save Changes
            await _dbContext.SaveChangesAsync();
            return "Success";
        }

        public async Task<string> RemoveBookFromToReadList(string UserId, int BookId)
        {
            // Summon User With Its Clr List (Eager Loading)
            var User = await _dbContext.Users
                .Include(clr => clr.ToReadList)
                .ThenInclude(b => b.Books)
                .FirstOrDefaultAsync(x => x.Id == UserId);

            // Getting The Book With Given Id
            var Book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == BookId);

            // Checking Values
            if (Book == null) return "This Book Is Not Found";
            if (User == null) return "This User Is Not Found";

            // Check If User Has This Book in the First 
            var IsBookExsists = User.ToReadList.Books.Any(i => i.Id == BookId);
            if (!IsBookExsists) return "Book Doesnot Even Exsists";

            // Adding 
            User.ToReadList.Books.Remove(Book);

            // Save Changes
            await _dbContext.SaveChangesAsync();
            return "Success";
        }

        public async Task<string> RemoveNoteToBook(string UserId,int BookId, int NoteId)
        {
            // Getting the User With The Given ID
            var User = await _dbContext.Users.Include(n => n.Notes).FirstOrDefaultAsync(i => i.Id == UserId);
            var Book = await _dbContext.Books.Include(n => n.Notes).FirstOrDefaultAsync(i => i.Id == BookId);
            // Getting The Note With The Given ID
            var Note = await _dbContext.Notes.FirstOrDefaultAsync(i => i.Id == NoteId);

            // Checking Values 
            if (User == null) return "User Is Not Found";
            if (Book == null) return "Book Is Not Found";
            if (Note == null) return "Note Is Not Found";


            //Adding 
            Book.Notes.Remove(Note);
            User.Notes.Remove(Note);
            // Save Changes
            await _dbContext.SaveChangesAsync();

            return "Success";
        }
        #endregion
    }
}
