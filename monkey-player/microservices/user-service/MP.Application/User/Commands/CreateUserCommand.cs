using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using MP.Data;
using MP.Domain.User;
using MP.Shared.Events.User;
using MVNormanNativeKit.Infrastructure.Core;
using MVNormanNativeKit.Infrastructure.Core.Commands;
using MVNormanNativeKit.Infrastructure.Core.Events;
using MVNormanNativeKit.Infrastructure.Data.EFCore.Core;

namespace MP.Application.User.Commands
{
    public class CreateUserCommand
    {
        public class Command : ICommand<Result>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
        }
        
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(cmd => cmd.FirstName).NotEmpty();
                RuleFor(cmd => cmd.LastName).NotEmpty();
                RuleFor(cmd => cmd.Email).NotEmpty().EmailAddress();
            }
        }
        
        public class Result
        {
            public Guid Id { get; set; }
        }

        public class Handler : ICommandHandler<Command, Result>
        {
            private readonly ILogger<CreateUserCommand> _logger;
            private readonly IEfUnitOfWork<UserDataContext> _context;
            private readonly IEventBus _eventBus;

            public Handler(
                ILogger<CreateUserCommand> logger,
                IEfUnitOfWork<UserDataContext> context, 
                IEventBus eventBus)
            {
                _logger = logger;
                _context = context;
                _eventBus = eventBus;
            }

            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                var user = new UserEntity()
                {
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    Email = command.Email
                };

                await _context
                    .RepositoryAsync<UserEntity, Guid>()
                    .AddAsync(user);

                var @event = Mapping.Map<UserEntity, UserCreatedEvent>(user);
                @event.Id = user.Id;
                
                // TODO: Cover under transaction 
                await _context.SaveChangesAsync(cancellationToken);
                
                // TODO: Configure mongo db or EF outbox
                //await _eventBus.CommitAsync(@event);

                _logger.LogInformation($"User has been successfully created with - {user.Id} identifier");
                
                var result = new Result
                {
                    Id = user.Id
                };

                return result;
            }
        }
    }
    
}