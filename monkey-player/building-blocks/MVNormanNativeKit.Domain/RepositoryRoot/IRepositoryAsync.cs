using System.Threading.Tasks;
using MVNormanNativeKit.Domain.EntityRoot;

namespace MVNormanNativeKit.Domain.RepositoryRoot
{
    public interface IRepositoryAsync<TEntity, TId> where TEntity : IEntity<TId>
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
    }
}
