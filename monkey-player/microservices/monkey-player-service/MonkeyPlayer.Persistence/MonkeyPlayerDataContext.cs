using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MonkeyPlayer.Domain.User;
using MonkeyPlayer.Domain.UserStatus;
using MVNormanNativeKit.Infrastructure.Core.Events;

namespace MonkeyPlayer.Persistence
{
    public class MonkeyPlayerDataContext: DbContext
    {
        private readonly IEventBus _eventBus;

        public MonkeyPlayerDataContext(DbContextOptions<MonkeyPlayerDataContext> options, IEventBus eventBus = null) : base(options)
        {
            _eventBus = eventBus;
        }
        
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserStatusReferenceEntity> UserStatusReferences { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MonkeyPlayerDataContext).Assembly);
        }
        
        public async Task SaveChangesAndCommit(IEvent @event)
        {
            using (var transaction = Database.BeginTransaction())
            {
                await SaveChangesAsync();
                await _eventBus.CommitAsync(@event);

                await transaction.CommitAsync();
            }
        }
    }
}