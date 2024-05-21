using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services
{
    public class CustomercustomerdemoService : ICRUDService<Customercustomerdemo>
    {
        public bool Insert(Customercustomerdemo obj)
        {
            var command = @"INSERT INTO ""CustomerCustomerDemo"" (customertypeid) VALUES (@customertypeid)";
            var parms = new { obj.customertypeid };
            var result =  General.db.ExecuteModel(command, parms);
            return result > 0;
        }

        public long InsertReturnId(Customercustomerdemo obj)
        {
            var command = @"INSERT INTO ""CustomerCustomerDemo"" (customertypeid) VALUES (@customertypeid)";
            var parms = new { obj.customertypeid };
            var result = General.db.ExecuteModel(command, parms);
            if (result > 0)
                return long.Parse(General.db.ExecuteScalar("SELECT last_insert_rowid()").ToString());
            else
                return 0;
        }

        public Customercustomerdemo Get(long id)
        {
            var command = @"SELECT * FROM ""CustomerCustomerDemo"" WHERE customerid = @customerid";
            var parms = new { customerid = id };
            return General.db.QueryFirst<Customercustomerdemo>(command, parms);
        }

        public List<Customercustomerdemo> GetList()
        {
            var command = @"SELECT * FROM ""CustomerCustomerDemo""";
            return General.db.Query<Customercustomerdemo>(command).ToList();
        }

        public bool Update(Customercustomerdemo obj)
        {
            var command = @"UPDATE ""CustomerCustomerDemo"" SET customertypeid = @customertypeid WHERE customerid = @customerid";
            var result = General.db.ExecuteModel(command, obj);
            return result > 0;
        }

        public bool Delete(long id)
        {
            var command = @"DELETE FROM ""CustomerCustomerDemo"" WHERE customerid = @customerid";
            var parms = new { customerid = id };
            var result = General.db.ExecuteModel(command, parms);
            return result > 0;
        }
    }
}