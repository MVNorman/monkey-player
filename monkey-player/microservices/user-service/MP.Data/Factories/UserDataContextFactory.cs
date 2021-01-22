using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MVNormanNativeKit.Infrastructure;

namespace MP.Data.Factories
{
    /// <summary>
    /// When your DbContext exists in another project, like a shared library. Meaning that is has no context to run from.
    /// EF Core migrations works by actually running your code, if that is not possible,
    /// then you need to provide information about how your DbContext can be created, which you do with this factory
    /// </summary>
    public class MonkeyPlayerDataContextFactory : IDesignTimeDbContextFactory<UserDataContext>
    {
        public UserDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserDataContext>();
            
            var connectionString = ConfigurationHelper
                .GetConfiguration(AppContext.BaseDirectory)
                ?.GetConnectionString(ConnectionStringKeys.App);

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new Exception("Connection string is not defined");
            
            optionsBuilder.UseSqlServer(connectionString,
                sql =>
                    sql.MigrationsAssembly(typeof(MonkeyPlayerDataContextFactory)
                        .GetTypeInfo()
                        .Assembly
                        .GetName()
                        .Name));

            return new UserDataContext(optionsBuilder.Options);
        }
    }
}