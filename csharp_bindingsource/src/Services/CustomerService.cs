using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services
{
    public class CustomerService : ICRUDService<Customer>
    {
        public bool Insert(Customer obj)
        {
            var command = @"INSERT INTO ""Customers"" (companyname, contactname, contacttitle, address, city, region, postalcode, country, phone, fax, supplierid) VALUES (@companyname, @contactname, @contacttitle, @address, @city, @region, @postalcode, @country, @phone, @fax, @supplierid)";
            var parms = new { obj.companyname, obj.contactname, obj.contacttitle, obj.address, obj.city, obj.region, obj.postalcode, obj.country, obj.phone, obj.fax, obj.supplierid };
            var result = General.db.ExecuteModel(command, parms);
            return result > 0;
        }

        public long InsertReturnId(Customer obj)
        {
            var command = @"INSERT INTO ""Customers"" (companyname, contactname, contacttitle, address, city, region, postalcode, country, phone, fax, supplierid) VALUES (@companyname, @contactname, @contacttitle, @address, @city, @region, @postalcode, @country, @phone, @fax, @supplierid)";
            var parms = new { obj.companyname, obj.contactname, obj.contacttitle, obj.address, obj.city, obj.region, obj.postalcode, obj.country, obj.phone, obj.fax, obj.supplierid };
            var result = General.db.ExecuteModel(command, parms);
            if (result > 0)
                return long.Parse(General.db.ExecuteScalar("SELECT last_insert_rowid()").ToString());
            else
                return 0;
        }

        public Customer Get(long id)
        {
            var command = @"SELECT * FROM ""Customers"" WHERE customerid = @customerid";
            var parms = new { customerid = id };
            return General.db.QueryFirst<Customer>(command, parms);
        }

        public List<Customer> GetList()
        {
            var command = @"SELECT * FROM ""Customers""";
            return General.db.Query<Customer>(command).ToList();
        }

        public bool Update(Customer obj)
        {
            var command = @"UPDATE ""Customers"" SET companyname = @companyname, contactname = @contactname, contacttitle = @contacttitle, address = @address, city = @city, region = @region, postalcode = @postalcode, country = @country, phone = @phone, fax = @fax, supplierid = @supplierid WHERE customerid = @customerid";
            var result = General.db.ExecuteModel(command, obj);
            return result > 0;
        }

        public bool Delete(long id)
        {
            var command = @"DELETE FROM ""Customers"" WHERE customerid = @customerid";
            var parms = new { customerid = id };
            var result = General.db.ExecuteModel(command, parms);
            return result > 0;
        }
    }
}