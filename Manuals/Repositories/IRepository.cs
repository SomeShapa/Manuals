using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manuals.Repositories
{
    public interface IRepository<T> where T : class
    {
        T GetById(object id);
        IEnumerable<T> GetAll();
        void Add(T item);
        void Update(T item);
        void Delete(object id);
        void Save();
    }
}
