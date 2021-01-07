using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MVNormanNativeKit.Domain.EventRoot;
using MVNormanNativeKit.Infrastructure.Bus.Messaging;
using MVNormanNativeKit.Infrastructure.Bus.Notifications;
using MVNormanNativeKit.Infrastructure.Data.EFCore.Core;

namespace MVNormanNativeKit.Infrastructure.Bus.InProc
{
    public class DomainEventDispatcher<TDbContext> : IDomainEventDispatcher where TDbContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly IEfUnitOfWork<TDbContext> _unitOfWork;

        public DomainEventDispatcher(IMediator mediator, IEfUnitOfWork<TDbContext> uow)
        {
            _mediator = mediator;
            _unitOfWork = uow;
        }

        public async Task Dispatch(IEvent @event)
        {
            var repository = _unitOfWork.RepositoryAsync<Outbox, Guid>();

            var outBox = new Outbox(
                @event.Id,
                @event.OccurredOn,
                @event);

            await repository.AddAsync(outBox);
            await _unitOfWork.SaveChangesAsync(default);

            await _mediator.Publish(new NotificationEnvelope(@event));
        }

        public void Dispose()
        {
        }
    }
}
