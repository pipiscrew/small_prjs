using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services
{
    public class EmployeeService : ICRUDService<Employee>
    {
        public bool Insert(Employee obj)
        {
            var command = @"INSERT INTO ""Employees"" (lastname, firstname, title, titleofcourtesy, birthdate, hiredate, address, city, region, postalcode, country, homephone, extension, photo, notes, reportsto, photopath) VALUES (@lastname, @firstname, @title, @titleofcourtesy, @birthdate, @hiredate, @address, @city, @region, @postalcode, @country, @homephone, @extension, @photo, @notes, @reportsto, @photopath)";
            var parms = new { obj.lastname, obj.firstname, obj.title, obj.titleofcourtesy, obj.birthdate, obj.hiredate, obj.address, obj.city, obj.region, obj.postalcode, obj.country, obj.homephone, obj.extension, obj.photo, obj.notes, obj.reportsto, obj.photopath };
            var result =  General.db.ExecuteModel(command, parms);
            return result > 0;
        }

        public long InsertReturnId(Employee obj)
        {
            var command = @"INSERT INTO ""Employees"" (lastname, firstname, title, titleofcourtesy, birthdate, hiredate, address, city, region, postalcode, country, homephone, extension, photo, notes, reportsto, photopath) VALUES (@lastname, @firstname, @title, @titleofcourtesy, @birthdate, @hiredate, @address, @city, @region, @postalcode, @country, @homephone, @extension, @photo, @notes, @reportsto, @photopath)";
            var parms = new { obj.lastname, obj.firstname, obj.title, obj.titleofcourtesy, obj.birthdate, obj.hiredate, obj.address, obj.city, obj.region, obj.postalcode, obj.country, obj.homephone, obj.extension, obj.photo, obj.notes, obj.reportsto, obj.photopath };
            var result = General.db.ExecuteModel(command, parms);
            if (result > 0)
                return long.Parse(General.db.ExecuteScalar("SELECT last_insert_rowid()").ToString());
            else
                return 0;
        }

        public Employee Get(long id)
        {
            var command = @"SELECT * FROM ""Employees"" WHERE employeeid = @employeeid";
            var parms = new { employeeid = id };
            return General.db.QueryFirst<Employee>(command, parms);
        }

        public List<Employee> GetList()
        {
            var command = @"SELECT * FROM ""Employees""";
            return General.db.Query<Employee>(command).ToList();
        }

        public bool Update(Employee obj)
        {
            var command = @"UPDATE ""Employees"" SET lastname = @lastname, firstname = @firstname, title = @title, titleofcourtesy = @titleofcourtesy, birthdate = @birthdate, hiredate = @hiredate, address = @address, city = @city, region = @region, postalcode = @postalcode, country = @country, homephone = @homephone, extension = @extension, photo = @photo, notes = @notes, reportsto = @reportsto, photopath = @photopath WHERE employeeid = @employeeid";
            var result = General.db.ExecuteModel(command, obj);
            return result > 0;
        }

        public bool Delete(long id)
        {
            var command = @"DELETE FROM ""Employees"" WHERE employeeid = @employeeid";
            var parms = new { employeeid = id };
            var result = General.db.ExecuteModel(command, parms);
            return result > 0;
        }
    }
}