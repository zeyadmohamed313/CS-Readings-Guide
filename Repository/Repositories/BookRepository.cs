using Core.Entites;
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
    public class BookRepository:GenericRepository<Book>
    {
        #region Fields 
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<Book> _books;
        #endregion
        #region Constructor
        public BookRepository(AppDbContext appDbContext, DbSet<Book> dbset):base(dbset,appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
    }
}
