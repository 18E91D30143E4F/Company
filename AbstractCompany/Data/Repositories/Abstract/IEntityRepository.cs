using System.Collections.Generic;

namespace Data.Repositories.Abstract
{
    public interface IEntityRepository<T> where T : class, new()
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(int id);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}