using Core.Entites;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BookRepository:GenericRepository<Book>,IBookRepository
    {
        #region Fields 
        private readonly AppDbContext _appDbContext;
        #endregion
        #region Constructor
        public BookRepository(AppDbContext appDbContext):base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion

        #region HandleFunctions
        public async Task<List<Book>> GetBooksWithCategory(int CategoryId)
        {
            // Checking For The Category
            var category = await _appDbContext.Categories
                .Include(b => b.Books)
                .FirstOrDefaultAsync(c=>c.Id==CategoryId);

            // Checking Null To Not Access Null 
            if (category == null) return null;

            return category.Books;
        }
        #endregion
    }
}
