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
    public class CategoryRepository:GenericRepository<Category>
    {
        #region Fields 
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<Category> _books;
        #endregion
        #region Constructor
        public CategoryRepository(AppDbContext appDbContext, DbSet<Category> dbset) : base(dbset, appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
    }
}
