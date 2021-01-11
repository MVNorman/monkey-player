using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVNormanNativeKit.Domain.EntityRoot;
using MVNormanNativeKit.Domain.RepositoryRoot;

namespace MVNormanNativeKit.Infrastructure.Data.EFCore.Core
{
    public class RepositoryAsync<TEntity, TId> : RepositoryAsync<DbContext, TEntity, TId>, IRepositoryAsync<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        public RepositoryAsync(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public class RepositoryAsync<TDbContext, TEntity, TId> : IRepositoryAsync<TEntity, TId>
        where TDbContext : DbContext
        where TEntity : class, IEntity<TId>
    {
        private readonly TDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public RepositoryAsync(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            var entry = _dbSet.Remove(entity);
            return await Task.FromResult(1); //TODO: find a better way
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var entry = _dbContext.Entry(entity);
            entry.State = EntityState.Modified;
            return await Task.FromResult(entry.Entity);
        }
    }
}
