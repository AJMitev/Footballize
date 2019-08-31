namespace Footballize.Web.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using Common.FileTypeChecker;
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
            if (file == null) return ValidationResult.Success;


            var checker = new FileTypeChecker();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);

                var fileType = checker.GetFileType(stream);

                if (!extensions.Contains(fileType.Extension.ToLower()))
                {
                    return new ValidationResult(InvalidExtensionErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}