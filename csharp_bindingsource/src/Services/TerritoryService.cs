using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services
{
    public class TerritoryService : ICRUDService<Territory>
    {
        public bool Insert(Territory obj)
        {
            var command = @"INSERT INTO ""Territories"" (territorydescription, regionid) VALUES (@territorydescription, @regionid)";
            var parms = new { obj.territorydescription, obj.regionid };
            var result =  General.db.ExecuteModel(command, parms);
            return result > 0;
        }

        public long InsertReturnId(Territory obj)
        {
            var command = @"INSERT INTO ""Territories"" (territorydescription, regionid) VALUES (@territorydescription, @regionid)";
            var parms = new { obj.territorydescription, obj.regionid };
            var result = General.db.ExecuteModel(command, parms);
            if (result > 0)
                return long.Parse(General.db.ExecuteScalar("SELECT last_insert_rowid()").ToString());
            else
                return 0;
        }

        public Territory Get(long id)
        {
            var command = @"SELECT * FROM ""Territories"" WHERE territoryid = @territoryid";
            var parms = new { territoryid = id };
            return General.db.QueryFirst<Territory>(command, parms);
        }

        public List<Territory> GetList()
        {
            var command = @"SELECT * FROM ""Territories""";
            return General.db.Query<Territory>(command).ToList();
        }

        public bool Update(Territory obj)
        {
            var command = @"UPDATE ""Territories"" SET territorydescription = @territorydescription, regionid = @regionid WHERE territoryid = @territoryid";
            var result = General.db.ExecuteModel(command, obj);
            return result > 0;
        }

        public bool Delete(long id)
        {
            var command = @"DELETE FROM ""Territories"" WHERE territoryid = @territoryid";
            var parms = new { territoryid = id };
            var result = General.db.ExecuteModel(command, parms);
            return result > 0;
        }
    }
}