using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services
{
    public class CustomerdemographicService : ICRUDService<Customerdemographic>
    {
        public bool Insert(Customerdemographic obj)
        {
            var command = @"INSERT INTO ""CustomerDemographics"" (customerdesc) VALUES (@customerdesc)";
            var parms = new { obj.customerdesc };
            var result =  General.db.ExecuteModel(command, parms);
            return result > 0;
        }

        public long InsertReturnId(Customerdemographic obj)
        {
            var command = @"INSERT INTO ""CustomerDemographics"" (customerdesc) VALUES (@customerdesc)";
            var parms = new { obj.customerdesc };
            var result = General.db.ExecuteModel(command, parms);
            if (result > 0)
                return long.Parse(General.db.ExecuteScalar("SELECT last_insert_rowid()").ToString());
            else
                return 0;
        }

        public Customerdemographic Get(long id)
        {
            var command = @"SELECT * FROM ""CustomerDemographics"" WHERE customertypeid = @customertypeid";
            var parms = new { customertypeid = id };
            return General.db.QueryFirst<Customerdemographic>(command, parms);
        }

        public List<Customerdemographic> GetList()
        {
            var command = @"SELECT * FROM ""CustomerDemographics""";
            return General.db.Query<Customerdemographic>(command).ToList();
        }

        public bool Update(Customerdemographic obj)
        {
            var command = @"UPDATE ""CustomerDemographics"" SET customerdesc = @customerdesc WHERE customertypeid = @customertypeid";
            var result = General.db.ExecuteModel(command, obj);
            return result > 0;
        }

        public bool Delete(long id)
        {
            var command = @"DELETE FROM ""CustomerDemographics"" WHERE customertypeid = @customertypeid";
            var parms = new { customertypeid = id };
            var result = General.db.ExecuteModel(command, parms);
            return result > 0;
        }
    }
}