using System.Data;

namespace posokanei.Infrastructure.Database.Factory
{
    public interface ISqliteConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
