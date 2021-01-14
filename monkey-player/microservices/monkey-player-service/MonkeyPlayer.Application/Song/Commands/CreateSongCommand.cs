using System;
using System.Threading;
using System.Threading.Tasks;
using MonkeyPlayer.Domain.Song;
using MonkeyPlayer.Domain.Song.Contracts;
using MP.Shared.Events.Song;
using MVNormanNativeKit.Infrastructure.Core;
using MVNormanNativeKit.Infrastructure.Core.Commands;
using MVNormanNativeKit.Infrastructure.Core.Events;

namespace MonkeyPlayer.Application.Song.Commands
{
    public class CreateSongCommand: ICommand<CreateSongResult>
    {
        public string Name { get; set; }
        public DateTime? ReleasedAt { get; set; }
        public SongStyleType StyleType { get; set; }
        public byte[] SongInBytes { get; set; }
    }
    
    public class CreateSongResult
    {
        public Guid Id { get; set; }
    }

    public class Handler : ICommandHandler<CreateSongCommand, CreateSongResult>
    {
        private readonly ISongRepository _songRepository;
        private readonly IEventBus _eventBus;

        public Handler(ISongRepository songRepository, IEventBus eventBus)
        {
            _songRepository = songRepository;
            _eventBus = eventBus;
        }

        public async Task<CreateSongResult> Handle(CreateSongCommand command, CancellationToken cancellationToken)
        {
            // TODO: Use proper calculation of mp3/other files
            var songInBytes = command.SongInBytes;
            var durationInMinutes = songInBytes[0] * 2;
            
            var song = new SongEntity()
            {
                Name = command.Name,
                ReleasedAt = command.ReleasedAt,
                StyleType = command.StyleType,
                SongInBytes = command.SongInBytes,
                DurationInMinutes = durationInMinutes,
            };

            await _songRepository.RepositoryAsync().AddAsync(song);
            
            var @event = Mapping.Map<SongEntity, SongCreatedEvent>(song);
            
            // TODO: check
            @event.Id = song.Id;

            await _songRepository.SaveChangesAsync(cancellationToken);

            await _eventBus.CommitAsync(@event);
            
            var result = new CreateSongResult
            {
                Id = song.Id
            };

            return result;
        }
    }
}