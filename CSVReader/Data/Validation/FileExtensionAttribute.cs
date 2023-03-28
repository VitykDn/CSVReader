using System.ComponentModel.DataAnnotations;

namespace CSVReader.Data.Validation
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);

                string[] extensions = { "csv" };
                bool result = extensions.Any(x => extension.EndsWith(x));

                if (!result)
                {
                    return new ValidationResult("Дозволений формат: csv");
                }
            }

            return ValidationResult.Success;
        }
    }
}
