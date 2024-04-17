using Core.Entites;
using Core.Generic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IBookRepository:IGenericRepository<Book>
    {
    }
}
