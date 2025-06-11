using System.ComponentModel.DataAnnotations;

namespace CdService.Validators
{
    public class WeightFileValidator : ValidationAttribute
    {
        private readonly int _maximumWeightInMegaBytes;

        public WeightFileValidator(int MaximumWeightInMegaBytes)
        {
            _maximumWeightInMegaBytes = MaximumWeightInMegaBytes;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if (formFile.Length > _maximumWeightInMegaBytes * 1024 * 1024)
            {
                return new ValidationResult($"The file weight must not be greater than {_maximumWeightInMegaBytes}MB");
            }

            return ValidationResult.Success;
        }
    }
}
