using System.Data;

namespace MVNormanNativeKit.Infrastructure.Data.Dapper
{
    public interface IDynamicSqlConnectionFactory
    {
        IDbConnection GetOpenConnection(string dbConnString);
    }
}
