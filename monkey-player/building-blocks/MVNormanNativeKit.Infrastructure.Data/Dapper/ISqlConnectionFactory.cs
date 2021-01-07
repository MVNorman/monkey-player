using System.Data;

namespace MVNormanNativeKit.Infrastructure.Data.Dapper
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
