using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVNormanNativeKit.Domain;
using MVNormanNativeKit.Domain.EntityRoot;
using MVNormanNativeKit.Domain.RepositoryRoot;
using MVNormanNativeKit.Infrastructure.Core.Events;

namespace MVNormanNativeKit.Infrastructure.Data.EFCore.Core
{
    public interface IEfUnitOfWork : IUnitOfWork { }

    public interface IEfUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext { }

    public class EfUnitOfWork<TDbContext> : IEfUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        
        private readonly IEventBus _eventBus;
        
        private ConcurrentDictionary<string, object> _repositories;

        public EfUnitOfWork(TDbContext context, IEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
        }

        public IQueryRepository<TEntity, TId> QueryRepository<TEntity, TId>() where TEntity : class, IEntity<TId>
        {
            if (_repositories == null)
                _repositories = new ConcurrentDictionary<string, object>();

            var key = $"{typeof(TEntity)}-query";
            if (!_repositories.ContainsKey(key))
            {
                var cachedRepository = new QueryRepository<TEntity, TId>(_context);
                _repositories[key] = cachedRepository;
            }

            return (IQueryRepository<TEntity, TId>)_repositories[key];
        }

        public virtual IRepositoryAsync<TEntity, TId> RepositoryAsync<TEntity, TId>() where TEntity : class, IEntity<TId>
        {
            if (_repositories == null) _repositories = new ConcurrentDictionary<string, object>();

            var key = $"{typeof(TEntity)}-command";
            if (!_repositories.ContainsKey(key))
                _repositories[key] = new RepositoryAsync<DbContext, TEntity, TId>(_context);

            return (IRepositoryAsync<TEntity, TId>)_repositories[key];
        }

        public virtual int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
