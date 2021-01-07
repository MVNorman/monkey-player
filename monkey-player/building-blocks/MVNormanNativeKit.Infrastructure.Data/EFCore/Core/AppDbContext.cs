using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVNormanNativeKit.Domain.EntityRoot;
using MVNormanNativeKit.Domain.EventRoot;
using MVNormanNativeKit.Tools.Extensions;

namespace MVNormanNativeKit.Infrastructure.Data.EFCore.Core
{
    public abstract class ApplicationDbContext : DbContext
    {
        private readonly IEnumerable<IDomainEventDispatcher> _eventBuses = null;

        protected ApplicationDbContext(DbContextOptions options, IEnumerable<IDomainEventDispatcher> eventBuses = null)
            : base(options)
        {
            _eventBuses = eventBuses;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            if (result > 0)
            {
                //TODO: Implement events mechanism
                //SaveChangesWithEvents();
            }

            return result;
        }

        public override int SaveChanges()
        {
            var result = base.SaveChanges();

            if (result > 0)
            {
                SaveChangesWithEvents();
            }

            return result;
        }

        /// <summary>
        /// Source: https://github.com/ardalis/CleanArchitecture/blob/master/src/CleanArchitecture.Infrastructure/Data/AppDbContext.cs
        /// </summary>
        private void SaveChangesWithEvents()
        {
            var entities = ChangeTracker
                .Entries()
                .Select(e => e.Entity)
                .Where(e => e.GetType().BaseType.IsGenericType
                    && e.GetType().BaseType.GetGenericTypeDefinition().IsAssignableFrom(typeof(AggregateRootBase<>)))
                .Where(e => e.ToDynamic().GetUncommittedEvents().Count > 0)
                .ToArray();

            foreach (var entity in entities)
            {
                var rootAggregator = entity.ToDynamic();
                var @events = rootAggregator.GetUncommittedEvents();
                foreach (var @event in @events)
                    _eventBuses.Select(b => b.Dispatch(@event)).ToList();

                rootAggregator.ClearUncommittedEvents();
            }
        }
    }
}
