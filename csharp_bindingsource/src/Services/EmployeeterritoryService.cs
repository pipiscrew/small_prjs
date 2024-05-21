using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services
{
    public class EmployeeterritoryService : ICRUDService<Employeeterritory>
    {
        public bool Insert(Employeeterritory obj)
        {
            var command = @"INSERT INTO ""EmployeeTerritories"" (territoryid) VALUES (@territoryid)";
            var parms = new { obj.territoryid };
            var result =  General.db.ExecuteModel(command, parms);
            return result > 0;
        }

        public long InsertReturnId(Employeeterritory obj)
        {
            var command = @"INSERT INTO ""EmployeeTerritories"" (territoryid) VALUES (@territoryid)";
            var parms = new { obj.territoryid };
            var result = General.db.ExecuteModel(command, parms);
            if (result > 0)
                return long.Parse(General.db.ExecuteScalar("SELECT last_insert_rowid()").ToString());
            else
                return 0;
        }

        public Employeeterritory Get(long id)
        {
            var command = @"SELECT * FROM ""EmployeeTerritories"" WHERE employeeid = @employeeid";
            var parms = new { employeeid = id };
            return General.db.QueryFirst<Employeeterritory>(command, parms);
        }

        public List<Employeeterritory> GetList()
        {
            var command = @"SELECT * FROM ""EmployeeTerritories""";
            return General.db.Query<Employeeterritory>(command).ToList();
        }

        public bool Update(Employeeterritory obj)
        {
            var command = @"UPDATE ""EmployeeTerritories"" SET territoryid = @territoryid WHERE employeeid = @employeeid";
            var result = General.db.ExecuteModel(command, obj);
            return result > 0;
        }

        public bool Delete(long id)
        {
            var command = @"DELETE FROM ""EmployeeTerritories"" WHERE employeeid = @employeeid";
            var parms = new { employeeid = id };
            var result = General.db.ExecuteModel(command, parms);
            return result > 0;
        }
    }
}