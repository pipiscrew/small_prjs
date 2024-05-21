using System.Collections.Generic;

namespace Services
{
    public interface ICRUDService<T> where T : class
    {
        bool Insert(T obj);
        long InsertReturnId(T obj);
        T Get(long id);
        List<T> GetList();
        bool Update(T obj);
        bool Delete(long id);
    }
}