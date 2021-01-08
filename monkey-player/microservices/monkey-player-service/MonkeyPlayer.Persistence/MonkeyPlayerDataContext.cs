using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MVNormanNativeKit.Domain.EventRoot;
using MVNormanNativeKit.Infrastructure.Data.EFCore.Core;

namespace MonkeyPlayer.Persistence
{
    public class MonkeyPlayerDataContext: ApplicationDbContext
    {
        public MonkeyPlayerDataContext(
            DbContextOptions options, 
            IEnumerable<IDomainEventDispatcher> eventBuses = null) : base(options, eventBuses)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MonkeyPlayerDataContext).Assembly);
        }
    }
}