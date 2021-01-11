using System.Linq;
using MVNormanNativeKit.Domain.EntityRoot;

namespace MVNormanNativeKit.Domain.RepositoryRoot
{
    public interface IQueryRepository<TEntity, TId> where TEntity : IEntity<TId>
    {
        IQueryable<TEntity> Queryable();
    }
}
