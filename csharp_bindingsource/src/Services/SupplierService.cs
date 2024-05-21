using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services
{
    public class SupplierService : ICRUDService<Supplier>
    {
        public bool Insert(Supplier obj)
        {
            var command = @"INSERT INTO ""Suppliers"" (companyname, contactname, contacttitle, address, city, region, postalcode, country, phone, fax, homepage) VALUES (@companyname, @contactname, @contacttitle, @address, @city, @region, @postalcode, @country, @phone, @fax, @homepage)";
            var parms = new { obj.companyname, obj.contactname, obj.contacttitle, obj.address, obj.city, obj.region, obj.postalcode, obj.country, obj.phone, obj.fax, obj.homepage };
            var result =  General.db.ExecuteModel(command, parms);
            return result > 0;
        }

        public long InsertReturnId(Supplier obj)
        {
            var command = @"INSERT INTO ""Suppliers"" (companyname, contactname, contacttitle, address, city, region, postalcode, country, phone, fax, homepage) VALUES (@companyname, @contactname, @contacttitle, @address, @city, @region, @postalcode, @country, @phone, @fax, @homepage)";
            var parms = new { obj.companyname, obj.contactname, obj.contacttitle, obj.address, obj.city, obj.region, obj.postalcode, obj.country, obj.phone, obj.fax, obj.homepage };
            var result = General.db.ExecuteModel(command, parms);
            if (result > 0)
                return long.Parse(General.db.ExecuteScalar("SELECT last_insert_rowid()").ToString());
            else
                return 0;
        }

        public Supplier Get(long id)
        {
            var command = @"SELECT * FROM ""Suppliers"" WHERE supplierid = @supplierid";
            var parms = new { supplierid = id };
            return General.db.QueryFirst<Supplier>(command, parms);
        }

        public List<Supplier> GetList()
        {
            var command = @"SELECT * FROM ""Suppliers""";
            return General.db.Query<Supplier>(command).ToList();
        }

        public List<Supplier> GetComboList()
        {
            var command = @"SELECT * FROM ""Suppliers""";
            var l = General.db.Query<Supplier>(command);
            l.Insert(0, new Supplier() { supplierid = 0, companyname = "" });
            return l.OrderBy(x => x.companyname).ToList();
        }

        public bool Update(Supplier obj)
        {
            var command = @"UPDATE ""Suppliers"" SET companyname = @companyname, contactname = @contactname, contacttitle = @contacttitle, address = @address, city = @city, region = @region, postalcode = @postalcode, country = @country, phone = @phone, fax = @fax, homepage = @homepage WHERE supplierid = @supplierid";
            var result = General.db.ExecuteModel(command, obj);
            return result > 0;
        }

        public bool Delete(long id)
        {
            var command = @"DELETE FROM ""Suppliers"" WHERE supplierid = @supplierid";
            var parms = new { supplierid = id };
            var result = General.db.ExecuteModel(command, parms);
            return result > 0;
        }
    }
}