using System.Linq;
using MVNormanNativeKit.Domain.EntityRoot;

namespace MVNormanNativeKit.Domain.RepositoryRoot
{
    public interface IQueryRepository<TEntity, TId> where TEntity : IAggregateRoot<TId>
    {
        IQueryable<TEntity> Queryable();
    }
}
