using CdService.DTOs;
using FluentValidation;

namespace CdService.Validators
{
    public class GroupInsertValidator : AbstractValidator<GroupInsertDTO>
    {
        public GroupInsertValidator()
        {
            RuleFor(x => x.NameGroup).NotEmpty().WithMessage("NameGroup is required");
            RuleFor(x => x.NameGroup).Length(2, 20).WithMessage("NameGroup must be between 2 and 20 characters");
            RuleFor(x => x.MusicGenreId).NotNull().WithMessage("MusicalGenreId is required");
        }
    }
}
