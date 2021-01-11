using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVNormanNativeKit.Domain.EntityRoot;
using MVNormanNativeKit.Domain.RepositoryRoot;
using MVNormanNativeKit.Infrastructure.Data.Dapper.SimpleCRUD;
using MVNormanNativeKit.Tools.Extensions;

namespace MVNormanNativeKit.Infrastructure.Data.Dapper.Core
{
    public class GenericRepository<TEntity, TId> : IRepositoryAsync<TEntity, TId>, IQueryRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }

        public GenericRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory;
        }

        public IQueryable<TEntity> Queryable()
        {
            using var conn = SqlConnectionFactory.GetOpenConnection();
            var entities = conn.GetList<TEntity>();
            return entities.AsQueryable();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            using var conn = SqlConnectionFactory.GetOpenConnection();
            var entities = await conn.GetListAsync<TEntity>(new { id });
            return entities.FirstOrDefault();
        }

        public async Task<IReadOnlyCollection<TEntity>> GetByConditionAsync(object whereConditions)
        {
            using var conn = SqlConnectionFactory.GetOpenConnection();
            var entities = await conn.GetListAsync<TEntity>(whereConditions);
            return entities.ToList();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using var conn = SqlConnectionFactory.GetOpenConnection();
            var newId = await conn.InsertAsync<TId, TEntity>(entity);
            if (entity is IEntity<TId> returnValue)
            {
                returnValue.ToDynamic().Id = newId;
                return (TEntity)returnValue;
            }

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using var conn = SqlConnectionFactory.GetOpenConnection();
            var numberRecordAffected = await conn.UpdateAsync(entity);
            if (numberRecordAffected <= 0)
            {
                throw new Exception("Could not update record to the database.");
            }

            return await GetByIdAsync(entity.Id);
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            using var conn = SqlConnectionFactory.GetOpenConnection();

            var numberRecordAffected = await conn.DeleteAsync(entity);

            if (numberRecordAffected <= 0)
            {
                throw new Exception("Could not delete record in the database.");
            }

            return numberRecordAffected;
        }
    }
}
