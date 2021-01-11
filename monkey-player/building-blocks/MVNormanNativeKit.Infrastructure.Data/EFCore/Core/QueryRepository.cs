using System.Linq;
using Microsoft.EntityFrameworkCore;
using MVNormanNativeKit.Domain.EntityRoot;
using MVNormanNativeKit.Domain.RepositoryRoot;

namespace MVNormanNativeKit.Infrastructure.Data.EFCore.Core
{
    public class QueryRepository<TEntity, TId> : QueryRepository<DbContext, TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        public QueryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public class QueryRepository<TDbContext, TEntity, TId> : IQueryRepository<TEntity, TId>
        where TDbContext : DbContext
        where TEntity : class, IEntity<TId>
    {
        private readonly TDbContext _dbContext;

        public QueryRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbContext.Set<TEntity>();
        }
    }
}
