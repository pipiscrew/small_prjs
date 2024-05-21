using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services
{
    public class OrderService : ICRUDService<Order>
    {
        public bool Insert(Order obj)
        {
            var command = @"INSERT INTO ""Orders"" (customerid, employeeid, orderdate, requireddate, shippeddate, shipvia, freight, shipname, shipaddress, shipcity, shipregion, shippostalcode, shipcountry) VALUES (@customerid, @employeeid, @orderdate, @requireddate, @shippeddate, @shipvia, @freight, @shipname, @shipaddress, @shipcity, @shipregion, @shippostalcode, @shipcountry)";
            var parms = new { obj.customerid, obj.employeeid, obj.orderdate, obj.requireddate, obj.shippeddate, obj.shipvia, obj.freight, obj.shipname, obj.shipaddress, obj.shipcity, obj.shipregion, obj.shippostalcode, obj.shipcountry };
            var result =  General.db.ExecuteModel(command, parms);
            return result > 0;
        }

        public long InsertReturnId(Order obj)
        {
            var command = @"INSERT INTO ""Orders"" (customerid, employeeid, orderdate, requireddate, shippeddate, shipvia, freight, shipname, shipaddress, shipcity, shipregion, shippostalcode, shipcountry) VALUES (@customerid, @employeeid, @orderdate, @requireddate, @shippeddate, @shipvia, @freight, @shipname, @shipaddress, @shipcity, @shipregion, @shippostalcode, @shipcountry)";
            var parms = new { obj.customerid, obj.employeeid, obj.orderdate, obj.requireddate, obj.shippeddate, obj.shipvia, obj.freight, obj.shipname, obj.shipaddress, obj.shipcity, obj.shipregion, obj.shippostalcode, obj.shipcountry };
            var result = General.db.ExecuteModel(command, parms);
            if (result > 0)
                return long.Parse(General.db.ExecuteScalar("SELECT last_insert_rowid()").ToString());
            else
                return 0;
        }

        public Order Get(long id)
        {
            var command = @"SELECT * FROM ""Orders"" WHERE orderid = @orderid";
            var parms = new { orderid = id };
            return General.db.QueryFirst<Order>(command, parms);
        }

        public List<Order> GetList()
        {
            var command = @"SELECT * FROM ""Orders""";
            return General.db.Query<Order>(command).ToList();
        }

        public bool Update(Order obj)
        {
            var command = @"UPDATE ""Orders"" SET customerid = @customerid, employeeid = @employeeid, orderdate = @orderdate, requireddate = @requireddate, shippeddate = @shippeddate, shipvia = @shipvia, freight = @freight, shipname = @shipname, shipaddress = @shipaddress, shipcity = @shipcity, shipregion = @shipregion, shippostalcode = @shippostalcode, shipcountry = @shipcountry WHERE orderid = @orderid";
            var result = General.db.ExecuteModel(command, obj);
            return result > 0;
        }

        public bool Delete(long id)
        {
            var command = @"DELETE FROM ""Orders"" WHERE orderid = @orderid";
            var parms = new { orderid = id };
            var result = General.db.ExecuteModel(command, parms);
            return result > 0;
        }
    }
}