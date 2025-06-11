using CdService.DTOs;
using FluentValidation;

namespace CdService.Validators
{
    public class GroupUpdateValidator : AbstractValidator<GroupUpdateDTO>
    {
        public GroupUpdateValidator()
        {
            RuleFor(x => x.IdGroup).NotNull().WithMessage("IdGroup is required");
            RuleFor(x => x.NameGroup).NotEmpty().WithMessage("NameGroup is required");
            RuleFor(x => x.NameGroup).Length(2, 20).WithMessage("NameGroup must be between 2 and 20 characters");
            RuleFor(x => x.MusicGenreId).NotNull().WithMessage("MusicalGenreId is required");
        }
    }
}
