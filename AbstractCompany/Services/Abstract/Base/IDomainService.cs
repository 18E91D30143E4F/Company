using System.Collections.Generic;

namespace Services.Abstract.Base
{
    public interface IDomainService<T> where T : class, new()
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(int id);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}