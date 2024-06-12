using System.Collections.Generic;

namespace NET472.Repository
{
    public interface IBaseRepo<T>
    {
        List<T> getAll();

        T findOne(T entity);

        T save(T entity);
    }
}