﻿using Core.Generic.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Fields
        private readonly DbSet<T> _dbSet;
        private readonly AppDbContext _appDbContext;
        #endregion
        #region Constructor
        public GenericRepository(DbSet<T> dbset, AppDbContext appDbContext)
        {
           _dbSet = dbset;
            _appDbContext = appDbContext;
        }
        #endregion
        #region HandleFunctions
        public async Task Add(T entity)
        {
            _dbSet.Add(entity); 
            await _appDbContext.SaveChangesAsync();
           
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _appDbContext.SaveChangesAsync();

        }
        #endregion
    }
}