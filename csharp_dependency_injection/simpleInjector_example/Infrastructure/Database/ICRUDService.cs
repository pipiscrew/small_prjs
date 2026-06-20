using System.Collections.Generic;

namespace posokanei.Infrastructure.Database
{
    public interface ICRUDService<T> where T : class
    {
        
        long InsertReturnId(T obj);
        List<T> GetList();
        //T Get(long id);
        //bool Insert(T obj);
        //bool Update(T obj);
        //bool Delete(long id);
    }
}
