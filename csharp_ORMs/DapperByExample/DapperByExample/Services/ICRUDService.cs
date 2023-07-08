using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperByExample.Services
{
    public interface ICRUDService<T> where T : class
    {
        bool Insert(T obj);
        T Get(long id);
        List<T> GetList();
        bool Update(T obj);
        bool Delete(long id);

        //Task<bool> Create(T obj);
        //Task<T> Get(Guid id);
        //Task<List<T>> GetList();
        //Task<T> Update(T obj);
        //Task<bool> Delete(Guid id);
    }
}
