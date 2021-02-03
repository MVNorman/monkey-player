using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MP.Shared.Events.User;
using MVNormanNativeKit.Infrastructure.Core.Commands;
using MVNormanNativeKit.Infrastructure.Core.Events;
using Player.Domain.Song.Contracts;

namespace Player.Application.Song.EventHandlers
{

    public class UserDeletedEventHandler : IEventHandler<UserDeletedEvent>
    {
        private readonly ICommandBus _commandBus;
        private readonly ISongRepository _songRepository;
        
        public UserDeletedEventHandler(ICommandBus commandBus, ISongRepository songRepository)
        {
            _commandBus = commandBus;
            _songRepository = songRepository;
        }

        public async Task Handle(UserDeletedEvent @event, CancellationToken cancellationToken)
        {
            var tt =await _songRepository.Queryable().ToListAsync();
            var a = 1;
            var k = a / 1;
            
            // var command = new DeleteReviewsByMovieIdCommand.Command
            // {
            //     MovieId = @event.MovieId
            // };
            //
            // await _commandBus.Send(command, cancellationToken);
        }
    }
}