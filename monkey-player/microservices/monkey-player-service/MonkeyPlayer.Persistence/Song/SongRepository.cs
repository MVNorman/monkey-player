using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MonkeyPlayer.Domain.Song;
using MonkeyPlayer.Domain.Song.Contracts;
using MVNormanNativeKit.Domain.EntityRoot;
using MVNormanNativeKit.Domain.RepositoryRoot;
using MVNormanNativeKit.Infrastructure.Data.EFCore.Core;

namespace MonkeyPlayer.Persistence.Song
{
    public class SongRepository: ISongRepository
    {
        private readonly IEfUnitOfWork<MonkeyPlayerDataContext> _context;

        public SongRepository(IEfUnitOfWork<MonkeyPlayerDataContext> context)
        {
            _context = context;
        }

        public IQueryable<SongEntity> Queryable()
        {
            return _context.QueryRepository<SongEntity, Guid>().Queryable();
        }

        public IRepositoryAsync<SongEntity, Guid> RepositoryAsync()
        {
            _context.SaveChanges();
            return _context.RepositoryAsync<SongEntity, Guid>();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}