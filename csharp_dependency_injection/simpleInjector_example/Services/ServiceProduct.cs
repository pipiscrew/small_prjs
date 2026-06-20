using posokanei.Entities;
using posokanei.Infrastructure.Database;
using posokanei.Infrastructure.Database.Factory;
using posokanei.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace posokanei.Services
{
    public class ServiceProduct : IServiceProduct, ICRUDService<Product>
    {
        private readonly ISqliteConnectionFactory _connectionFactory;
        private readonly Serilog.ILogger _logger;

        public ServiceProduct(ISqliteConnectionFactory connectionFactory, Serilog.ILogger logger)
        {
            this._connectionFactory = connectionFactory;
            this._logger = logger;
        }

        public long InsertReturnId(Product obj)
        {
            using (var db = _connectionFactory.CreateConnection())
            {
                db.Open();
                var command = @"INSERT INTO ""products"" (title, url, when2check, dateupdated) VALUES (@title, @url, @when2check, @dateupdated)";
                var parms = new { obj.title, obj.url, obj.when2check, obj.dateupdated };
                var result = db.ExecuteModel(command, parms);
                if (result > 0)
                    return long.Parse(db.ExecuteScalar("SELECT last_insert_rowid()").ToString());
                else
                    return 0;
            }
        }

        public List<Product> GetList()
        {
            using (var db = _connectionFactory.CreateConnection())
            {
                db.Open();

                _logger.Information("connection open");

                var command = @"SELECT * FROM ""products""";
                return db.Query<Product>(command).ToList();
            }

        }

    }
}
