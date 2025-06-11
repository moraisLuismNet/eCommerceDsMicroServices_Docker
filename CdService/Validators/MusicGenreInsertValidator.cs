using FluentValidation;
using CdService.DTOs;
namespace CdService.Validators
{
    public class MusicGenreInsertValidator : AbstractValidator<MusicGenreInsertDTO>
    {
        public MusicGenreInsertValidator()
        {
            RuleFor(x => x.NameMusicGenre).NotEmpty().WithMessage("NameMusicalGenre is required");
            RuleFor(x => x.NameMusicGenre).Length(2, 20).WithMessage("NameMusicalGenre must be between 2 and 20 characters");
        }
    }
}
