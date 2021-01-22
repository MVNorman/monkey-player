using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MP.Domain.User;
using MP.Domain.UserStatus;
using MVNormanNativeKit.Infrastructure.Core.Events;

namespace MP.Data
{
    public class UserDataContext: DbContext
    {
        private readonly IEventBus _eventBus;

        public UserDataContext(DbContextOptions<UserDataContext> options, IEventBus eventBus = null) : base(options)
        {
            _eventBus = eventBus;
        }

        public UserDataContext()
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserStatusReferenceEntity> UserStatusReferences { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDataContext).Assembly);
        }
    }
}