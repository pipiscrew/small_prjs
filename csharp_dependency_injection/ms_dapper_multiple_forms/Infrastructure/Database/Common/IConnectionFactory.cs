using System.Data;

namespace Infrastructure.Database.Common
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
