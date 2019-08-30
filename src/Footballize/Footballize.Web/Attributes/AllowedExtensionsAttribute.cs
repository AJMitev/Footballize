namespace Footballize.Web.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Http;

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private const string InvalidExtensionErrorMessage = "This extension is not allowed!";
        private readonly string[] extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            this.extensions = extensions;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            var extension = Path.GetExtension(file.FileName);

            if (!(file == null))
            {
                if (!extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(InvalidExtensionErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}