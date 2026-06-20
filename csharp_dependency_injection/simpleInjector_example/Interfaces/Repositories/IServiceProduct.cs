using posokanei.Entities;
using System.Collections.Generic;

namespace posokanei.Interfaces.Repositories
{
    public interface IServiceProduct
    {
        List<Product> GetList();
        long InsertReturnId(Product obj);
    }

}
