namespace Footballize.Web.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using FileTypeChecker;
    using Microsoft.AspNetCore.Http;

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private const string InvalidExtensionErrorMessage = "This extension is not allowed!";
        private readonly string[] extensions;

        public AllowedExtensionsAttribute(params string[] extensions)
        {
            this.extensions = extensions;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            if (!(value is IFormFile file)) return ValidationResult.Success;

            
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);

                if (!FileTypeValidator.IsTypeRecognizable(stream))
                {
                    return new ValidationResult(InvalidExtensionErrorMessage);
                }

                var fileType = FileTypeValidator.GetFileType(stream);

                if (!extensions.Contains(fileType.Extension.ToLower()))
                {
                    return new ValidationResult(InvalidExtensionErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}