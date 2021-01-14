using FluentValidation;
using MonkeyPlayer.Application.Song.Commands;

namespace MonkeyPlayer.Application.Song.Validators
{

    public class CreateSongValidator : AbstractValidator<CreateSongCommand>
    {
        public CreateSongValidator()
        {
            RuleFor(cmd => cmd.Name).NotEmpty();
            RuleFor(cmd => cmd.SongInBytes).NotEmpty();
        }
    }
}