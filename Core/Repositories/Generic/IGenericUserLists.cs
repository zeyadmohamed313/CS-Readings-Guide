using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.Generic
{
    public interface IGenericUserLists<T> where T : UserListBase
    {
        public Task AddBookToList();
        public Task RemoveBookFromList();
    }
}
