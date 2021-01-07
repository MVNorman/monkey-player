using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVNormanNativeKit.Domain.EntityRoot;
using MVNormanNativeKit.Domain.RepositoryRoot;
using MVNormanNativeKit.Infrastructure.Data.Dapper.SimpleCRUD;

namespace MVNormanNativeKit.Infrastructure.Data.Dapper.Core
{
    public static class GenericRepositoryExtensions
    {
        public static async Task<TEntity> GetByIdAsync<TEntity, TId>

        (this IQueryRepository<TEntity, TId> repository, TId id) where TEntity : class, IAggregateRoot<TId>
        {
            if (!(repository is GenericRepository<TEntity, TId> genericRepository))
            {
                throw new System.Exception("Make sure your IQueryRepository<TEntity, TId> is a GenericRepository<TEntity, TId> instance.");
            }

            using var connection = genericRepository.SqlConnectionFactory.GetOpenConnection();
            var entities = await connection.GetListAsync<TEntity>(new { id });
            return entities.FirstOrDefault();
        }

        public static async Task<IReadOnlyCollection<TEntity>> GetByConditionAsync<TEntity, TId>(this IQueryRepository<TEntity, TId> repository, object whereConditions)
            where TEntity : class, IAggregateRoot<TId>
        {
            if (!(repository is GenericRepository<TEntity, TId> genericRepository))
            {
                throw new System.Exception("Make sure your IQueryRepository<TEntity, TId> is a GenericRepository<TEntity, TId> instance.");
            }

            using var connection = genericRepository.SqlConnectionFactory.GetOpenConnection();
            var entities = await connection.GetListAsync<TEntity>(whereConditions);
            return entities.ToList();
        }
    }
}
