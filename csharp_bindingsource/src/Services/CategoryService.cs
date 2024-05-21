using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services
{
    public class CategoryService : ICRUDService<Category>
    {
        public bool Insert(Category obj)
        {
            var command = @"INSERT INTO ""Categories"" (categoryname, description, picture) VALUES (@categoryname, @description, @picture)";
            var parms = new { obj.categoryname, obj.description, obj.picture };
            var result =  General.db.ExecuteModel(command, parms);
            return result > 0;
        }

        public long InsertReturnId(Category obj)
        {
            var command = @"INSERT INTO ""Categories"" (categoryname, description, picture) VALUES (@categoryname, @description, @picture)";
            var parms = new { obj.categoryname, obj.description, obj.picture };
            var result = General.db.ExecuteModel(command, parms);
            if (result > 0)
                return long.Parse(General.db.ExecuteScalar("SELECT last_insert_rowid()").ToString());
            else
                return 0;
        }

        public Category Get(long id)
        {
            var command = @"SELECT * FROM ""Categories"" WHERE categoryid = @categoryid";
            var parms = new { categoryid = id };
            return General.db.QueryFirst<Category>(command, parms);
        }

        public List<Category> GetList()
        {
            var command = @"SELECT * FROM ""Categories""";
            return General.db.Query<Category>(command).ToList();
        }

        public bool Update(Category obj)
        {
            var command = @"UPDATE ""Categories"" SET categoryname = @categoryname, description = @description, picture = @picture WHERE categoryid = @categoryid";
            var result = General.db.ExecuteModel(command, obj);
            return result > 0;
        }

        public bool Delete(long id)
        {
            var command = @"DELETE FROM ""Categories"" WHERE categoryid = @categoryid";
            var parms = new { categoryid = id };
            var result = General.db.ExecuteModel(command, parms);
            return result > 0;
        }
    }
}