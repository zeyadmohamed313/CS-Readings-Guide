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
    public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
    {
        #region Fields 
        private readonly AppDbContext _appDbContext;
        #endregion
        #region Constructor
        public CategoryRepository(AppDbContext appDbContext) : base( appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
    }
}
