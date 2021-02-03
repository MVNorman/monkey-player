using FluentValidation;
using Player.Application.Song.Commands;

namespace Player.Application.Song.Validators
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