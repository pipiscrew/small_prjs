using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services
{
    public class Sqlite_sequenceService : ICRUDService<Sqlite_sequence>
    {
        public bool Insert(Sqlite_sequence obj)
        {
            var command = @"INSERT INTO ""sqlite_sequence"" (name, seq) VALUES (@name, @seq)";
            var parms = new { obj.name, obj.seq };
            var result =  General.db.ExecuteModel(command, parms);
            return result > 0;
        }

        public long InsertReturnId(Sqlite_sequence obj)
        {
            var command = @"INSERT INTO ""sqlite_sequence"" (name, seq) VALUES (@name, @seq)";
            var parms = new { obj.name, obj.seq };
            var result = General.db.ExecuteModel(command, parms);
            if (result > 0)
                return long.Parse(General.db.ExecuteScalar("SELECT last_insert_rowid()").ToString());
            else
                return 0;
        }

        public Sqlite_sequence Get(long id)
        {
            var command = @"SELECT * FROM ""sqlite_sequence"" WHERE primarykey_not_found = @primarykey_not_found";
            var parms = new { primarykey_not_found = id };
            return General.db.QueryFirst<Sqlite_sequence>(command, parms);
        }

        public List<Sqlite_sequence> GetList()
        {
            var command = @"SELECT * FROM ""sqlite_sequence""";
            return General.db.Query<Sqlite_sequence>(command).ToList();
        }

        public bool Update(Sqlite_sequence obj)
        {
            var command = @"UPDATE ""sqlite_sequence"" SET name = @name, seq = @seq WHERE primarykey_not_found = @primarykey_not_found";
            var result = General.db.ExecuteModel(command, obj);
            return result > 0;
        }

        public bool Delete(long id)
        {
            var command = @"DELETE FROM ""sqlite_sequence"" WHERE primarykey_not_found = @primarykey_not_found";
            var parms = new { primarykey_not_found = id };
            var result = General.db.ExecuteModel(command, parms);
            return result > 0;
        }
    }
}