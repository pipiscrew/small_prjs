using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services
{
    public class ProductService : ICRUDService<Product>
    {
        public bool Insert(Product obj)
        {
            var command = @"INSERT INTO ""Products"" (productname, supplierid, categoryid, quantityperunit, unitprice, unitsinstock, unitsonorder, reorderlevel, discontinued) VALUES (@productname, @supplierid, @categoryid, @quantityperunit, @unitprice, @unitsinstock, @unitsonorder, @reorderlevel, @discontinued)";
            var parms = new { obj.productname, obj.supplierid, obj.categoryid, obj.quantityperunit, obj.unitprice, obj.unitsinstock, obj.unitsonorder, obj.reorderlevel, obj.discontinued };
            var result =  General.db.ExecuteModel(command, parms);
            return result > 0;
        }

        public long InsertReturnId(Product obj)
        {
            var command = @"INSERT INTO ""Products"" (productname, supplierid, categoryid, quantityperunit, unitprice, unitsinstock, unitsonorder, reorderlevel, discontinued) VALUES (@productname, @supplierid, @categoryid, @quantityperunit, @unitprice, @unitsinstock, @unitsonorder, @reorderlevel, @discontinued)";
            var parms = new { obj.productname, obj.supplierid, obj.categoryid, obj.quantityperunit, obj.unitprice, obj.unitsinstock, obj.unitsonorder, obj.reorderlevel, obj.discontinued };
            var result = General.db.ExecuteModel(command, parms);
            if (result > 0)
                return long.Parse(General.db.ExecuteScalar("SELECT last_insert_rowid()").ToString());
            else
                return 0;
        }

        public Product Get(long id)
        {
            var command = @"SELECT * FROM ""Products"" WHERE productid = @productid";
            var parms = new { productid = id };
            return General.db.QueryFirst<Product>(command, parms);
        }

        public List<Product> GetList()
        {
            var command = @"SELECT * FROM ""Products""";
            return General.db.Query<Product>(command).ToList();
        }

        public bool Update(Product obj)
        {
            var command = @"UPDATE ""Products"" SET productname = @productname, supplierid = @supplierid, categoryid = @categoryid, quantityperunit = @quantityperunit, unitprice = @unitprice, unitsinstock = @unitsinstock, unitsonorder = @unitsonorder, reorderlevel = @reorderlevel, discontinued = @discontinued WHERE productid = @productid";
            var result = General.db.ExecuteModel(command, obj);
            return result > 0;
        }

        public bool Delete(long id)
        {
            var command = @"DELETE FROM ""Products"" WHERE productid = @productid";
            var parms = new { productid = id };
            var result = General.db.ExecuteModel(command, parms);
            return result > 0;
        }
    }
}