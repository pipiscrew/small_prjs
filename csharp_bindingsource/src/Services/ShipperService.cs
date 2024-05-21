using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services
{
    public class ShipperService : ICRUDService<Shipper>
    {
        public bool Insert(Shipper obj)
        {
            var command = @"INSERT INTO ""Shippers"" (companyname, phone) VALUES (@companyname, @phone)";
            var parms = new { obj.companyname, obj.phone };
            var result =  General.db.ExecuteModel(command, parms);
            return result > 0;
        }

        public long InsertReturnId(Shipper obj)
        {
            var command = @"INSERT INTO ""Shippers"" (companyname, phone) VALUES (@companyname, @phone)";
            var parms = new { obj.companyname, obj.phone };
            var result = General.db.ExecuteModel(command, parms);
            if (result > 0)
                return long.Parse(General.db.ExecuteScalar("SELECT last_insert_rowid()").ToString());
            else
                return 0;
        }

        public Shipper Get(long id)
        {
            var command = @"SELECT * FROM ""Shippers"" WHERE shipperid = @shipperid";
            var parms = new { shipperid = id };
            return General.db.QueryFirst<Shipper>(command, parms);
        }

        public List<Shipper> GetList()
        {
            var command = @"SELECT * FROM ""Shippers""";
            return General.db.Query<Shipper>(command).ToList();
        }

        public bool Update(Shipper obj)
        {
            var command = @"UPDATE ""Shippers"" SET companyname = @companyname, phone = @phone WHERE shipperid = @shipperid";
            var result = General.db.ExecuteModel(command, obj);
            return result > 0;
        }

        public bool Delete(long id)
        {
            var command = @"DELETE FROM ""Shippers"" WHERE shipperid = @shipperid";
            var parms = new { shipperid = id };
            var result = General.db.ExecuteModel(command, parms);
            return result > 0;
        }
    }
}