using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET7NIVSUP.Repository
{
    public interface IBaseRepo<T>
    {
        List<T> getAll();

        T findOne(T entity);

        T save(T entity);

        bool delete(T entity);
    }
}
