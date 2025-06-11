using System.ComponentModel.DataAnnotations;

namespace CdService.Validators
{
    public class FileTypeValidator : ValidationAttribute
    {
        private readonly string[] _validTypes;

        public FileTypeValidator(string[] validTypes)
        {
            _validTypes = validTypes;
        }

        public FileTypeValidator(GroupFileType groupFileType)
        {
            if (groupFileType == GroupFileType.Image)
            {
                _validTypes = new string[] { "image/jpeg", "image/png", "image/gif" };
            }
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

            if (!_validTypes.Contains(formFile.ContentType))
            {
                return new ValidationResult($"The file type must be one of the following: {string.Join(", ", _validTypes)}");
            }

            return ValidationResult.Success;
        }
    }
}
