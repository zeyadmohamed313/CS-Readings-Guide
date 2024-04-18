using Core;
using Core.Entites;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Feilds
        public IBookRepository bookRepository { get; private set; }

        public ICategoryRepository categoryRepository { get; private set; }
        public IUserActivityRepository userActivityRepository { get; private set; }
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<Book> _books;
        private readonly DbSet<Category> _categories;

        #endregion

        #region Constructor 
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        

            bookRepository = new BookRepository(_appDbContext);
            categoryRepository = new CategoryRepository(_appDbContext);
            userActivityRepository = new UserActivityRepository(_appDbContext);
        }
        #endregion

        #region HandleFunctions
        public  void commit()
        {
            _appDbContext.SaveChanges();
        }

        public async void Dispose()
        {
            await _appDbContext.DisposeAsync();
        }
        #endregion
    }
}
