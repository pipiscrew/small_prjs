using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services
{
    public class OrderdetailService : ICRUDService<Orderdetail>
    {
        public bool Insert(Orderdetail obj)
        {
            var command = @"INSERT INTO ""Orderdetails"" (productid, unitprice, quantity, discount) VALUES (@productid, @unitprice, @quantity, @discount)";
            var parms = new { obj.productid, obj.unitprice, obj.quantity, obj.discount };
            var result =  General.db.ExecuteModel(command, parms);
            return result > 0;
        }

        public long InsertReturnId(Orderdetail obj)
        {
            var command = @"INSERT INTO ""Orderdetails"" (productid, unitprice, quantity, discount) VALUES (@productid, @unitprice, @quantity, @discount)";
            var parms = new { obj.productid, obj.unitprice, obj.quantity, obj.discount };
            var result = General.db.ExecuteModel(command, parms);
            if (result > 0)
                return long.Parse(General.db.ExecuteScalar("SELECT last_insert_rowid()").ToString());
            else
                return 0;
        }

        public Orderdetail Get(long id)
        {
            var command = @"SELECT * FROM ""Orderdetails"" WHERE orderid = @orderid";
            var parms = new { orderid = id };
            return General.db.QueryFirst<Orderdetail>(command, parms);
        }

        public List<Orderdetail> GetList()
        {
            var command = @"SELECT * FROM ""Orderdetails""";
            return General.db.Query<Orderdetail>(command).ToList();
        }

        public bool Update(Orderdetail obj)
        {
            var command = @"UPDATE ""Orderdetails"" SET productid = @productid, unitprice = @unitprice, quantity = @quantity, discount = @discount WHERE orderid = @orderid";
            var result = General.db.ExecuteModel(command, obj);
            return result > 0;
        }

        public bool Delete(long id)
        {
            var command = @"DELETE FROM ""Orderdetails"" WHERE orderid = @orderid";
            var parms = new { orderid = id };
            var result = General.db.ExecuteModel(command, parms);
            return result > 0;
        }
    }
}