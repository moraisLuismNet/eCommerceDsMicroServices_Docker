using FluentValidation;
using CdService.DTOs;

namespace CdService.Validators
{
    public class MusicGenreUpdateValidator: AbstractValidator<MusicGenreUpdateDTO>
    {
        public MusicGenreUpdateValidator()
        {
            RuleFor(x => x.IdMusicGenre).NotNull().WithMessage("IdMusicalGenre is required");
            RuleFor(x => x.NameMusicGenre).NotEmpty().WithMessage("NameMusicalGenre is required");
            RuleFor(x => x.NameMusicGenre).Length(2, 20).WithMessage("NameMusicalGenre must be between 2 and 20 characters");
        }
    }
}
