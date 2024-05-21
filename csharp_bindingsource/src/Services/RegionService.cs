using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services
{
    public class RegionService : ICRUDService<Region>
    {
        public bool Insert(Region obj)
        {
            var command = @"INSERT INTO ""Regions"" (regiondescription) VALUES (@regiondescription)";
            var parms = new { obj.regiondescription };
            var result =  General.db.ExecuteModel(command, parms);
            return result > 0;
        }

        public long InsertReturnId(Region obj)
        {
            var command = @"INSERT INTO ""Regions"" (regiondescription) VALUES (@regiondescription)";
            var parms = new { obj.regiondescription };
            var result = General.db.ExecuteModel(command, parms);
            if (result > 0)
                return long.Parse(General.db.ExecuteScalar("SELECT last_insert_rowid()").ToString());
            else
                return 0;
        }

        public Region Get(long id)
        {
            var command = @"SELECT * FROM ""Regions"" WHERE regionid = @regionid";
            var parms = new { regionid = id };
            return General.db.QueryFirst<Region>(command, parms);
        }

        public List<Region> GetList()
        {
            var command = @"SELECT * FROM ""Regions""";
            return General.db.Query<Region>(command).ToList();
        }

        public bool Update(Region obj)
        {
            var command = @"UPDATE ""Regions"" SET regiondescription = @regiondescription WHERE regionid = @regionid";
            var result = General.db.ExecuteModel(command, obj);
            return result > 0;
        }

        public bool Delete(long id)
        {
            var command = @"DELETE FROM ""Regions"" WHERE regionid = @regionid";
            var parms = new { regionid = id };
            var result = General.db.ExecuteModel(command, parms);
            return result > 0;
        }
    }
}