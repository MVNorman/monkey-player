using MVNormanNativeKit.Domain.EntityRoot;

namespace MVNormanNativeKit.Domain.RepositoryRoot
{
    public interface IRepositoryFactory
    {
        IQueryRepository<TEntity, TId> QueryRepository<TEntity, TId>() where TEntity : class, IAggregateRoot<TId>;
        IRepositoryAsync<TEntity, TId> RepositoryAsync<TEntity, TId>() where TEntity : class, IAggregateRoot<TId>;
    }
}
