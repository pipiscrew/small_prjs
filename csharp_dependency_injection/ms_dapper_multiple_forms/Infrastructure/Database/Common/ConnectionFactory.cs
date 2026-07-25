using System;
using System.Data;
using System.Data.SQLite;

namespace Infrastructure.Database.Common
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connestionString;

        public ConnectionFactory(string connestionString)
        {
            //ArgumentException.ThrowIfNullOrWhiteSpace(connestionString);
            if (string.IsNullOrWhiteSpace(connestionString))
                throw new ArgumentException("Missing connection string");

            _connestionString = connestionString;
        }

        public IDbConnection CreateConnection() {
           return new SQLiteConnection(_connestionString);
        }
    }
}
