using System;
using System.Threading;
using System.Threading.Tasks;
using MVNormanNativeKit.Domain.RepositoryRoot;

namespace MVNormanNativeKit.Domain
{
    public interface IUnitOfWork : IRepositoryFactory, IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
