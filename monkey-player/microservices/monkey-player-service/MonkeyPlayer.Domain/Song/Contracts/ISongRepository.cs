using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MVNormanNativeKit.Domain.RepositoryRoot;

namespace MonkeyPlayer.Domain.Song.Contracts
{
    public interface ISongRepository
    {
        IQueryable<SongEntity> Queryable();
        IRepositoryAsync<SongEntity, Guid> RepositoryAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}