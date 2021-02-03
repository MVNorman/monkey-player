using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MP.Data;
using MP.Domain.User;
using MP.Shared.Events.User;
using MVNormanNativeKit.Infrastructure.Core;
using MVNormanNativeKit.Infrastructure.Core.Commands;
using MVNormanNativeKit.Infrastructure.Core.Events;
using MVNormanNativeKit.Infrastructure.Data.EFCore.Core;

namespace MP.Application.User.Commands
{
    public class DeleteUserCommand
    {
        public class Command : ICommand<Result>
        {
            public Guid Id { get; set; }
        }

        public class Result
        {
            public string ResultMessage { get; set; } = "User has been successfully deleted";
        }
        
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : ICommandHandler<Command, Result>
        {
            private readonly IEfUnitOfWork<UserDataContext> _context;
            private readonly IEventBus _eventBus;

            public Handler(IEfUnitOfWork<UserDataContext> context, IEventBus eventBus)
            {
                _context = context;
                _eventBus = eventBus;
            }

            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                var result = new Result();
                
                var user = await _context.QueryRepository<UserEntity, Guid>()
                    .Queryable()
                    .FirstOrDefaultAsync(x=> x.Id == command.Id, cancellationToken);

                if (user is null)
                {
                    result.ResultMessage = "Unknown user identifier";
                    return result;
                }
                
                await _context.RepositoryAsync<UserEntity, Guid>().DeleteAsync(user);
                
                var @event = Mapping.Map<UserEntity, UserDeletedEvent>(user);
                @event.UserId = user.Id;

                await _context.SaveChangesAsync(cancellationToken);
                await _eventBus.CommitAsync(@event);
                
                // await using var context = new UserDataContext();
                // await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
                //
                // try
                // {
                //     await _context.SaveChangesAsync(cancellationToken);
                //
                //     await _eventBus.CommitAsync(@event);
                //             
                //     await transaction.CommitAsync(cancellationToken);
                // }
                // catch (Exception)
                // {
                //     await transaction.RollbackAsync(cancellationToken);
                // }

                return result;  
            }
        }
    }
}