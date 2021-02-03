using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVNormanNativeKit.Infrastructure.Core.Events;
using Player.Domain.Song;

namespace Player.Data
{
    public class MonkeyPlayerDataContext: DbContext
    {
        private readonly IEventBus _eventBus;

        public MonkeyPlayerDataContext(DbContextOptions<MonkeyPlayerDataContext> options, IEventBus eventBus = null) : base(options)
        {
            _eventBus = eventBus;
        }
        
        public DbSet<SongEntity> Songs { get; set; }
        
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