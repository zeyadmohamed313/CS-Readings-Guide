using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IUnitOfWork:IDisposable
    {
        public IBookRepository bookRepository { get;}
        public ICategoryRepository categoryRepository { get; }
        public IUserActivityRepository userActivityRepository { get; }
        public void commit();
    }
}
