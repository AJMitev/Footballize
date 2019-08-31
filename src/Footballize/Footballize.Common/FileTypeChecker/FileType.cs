namespace Footballize.Common.FileTypeChecker
{
    using System.IO;

    public class FileType : IFileType
    {
        private readonly IFileTypeMatcher fileTypeMatcher;

        public string Name { get; }

        public string Extension { get; }

        public static IFileType Unknown { get; } = new FileType("unknown", string.Empty, null);

        public FileType(string name, string extension, IFileTypeMatcher matcher)
        {
            this.Name = name;
            this.Extension = extension;
            this.fileTypeMatcher = matcher;
        }

        public bool Matches(Stream stream)
        {
            return this.fileTypeMatcher == null || this.fileTypeMatcher.Matches(stream);
        }
    }
}