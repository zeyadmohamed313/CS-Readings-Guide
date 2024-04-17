using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Generic.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T> GetAsync(int id);
        public Task<List<T>> GetAllAsync();
        public Task Add(T entity);
        public Task Update(T entity);
        public Task DeleteAsync(int id);
    }
}
