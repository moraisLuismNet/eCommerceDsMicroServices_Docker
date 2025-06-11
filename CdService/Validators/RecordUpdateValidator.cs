using CdService.DTOs;
using FluentValidation;

namespace CdService.Validators
{
    public class RecordUpdateValidator : AbstractValidator<RecordUpdateDTO>
    {
        public RecordUpdateValidator()
        {
            RuleFor(x => x.IdRecord).NotNull().WithMessage("IdRecord is required");
            RuleFor(x => x.TitleRecord).NotEmpty().WithMessage("TitleRecord is required");
            RuleFor(x => x.TitleRecord).Length(2, 20).WithMessage("TitleRecord must be between 2 and 20 characters");
            RuleFor(x => x.YearOfPublication).NotEmpty().WithMessage("YearOfPublication is required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Stock is required");
            RuleFor(x => x.GroupId).NotEmpty().WithMessage("GroupId is required");
        }
    }
}
