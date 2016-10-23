using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gravel.core.Database;

namespace gravel.service.Domain
{
    public interface IRepository<T>
    {
        IList<T> GetAll();

        void Insert(T entity);

        void Update(T entity);

        //T GetById(int id);
    }
}
