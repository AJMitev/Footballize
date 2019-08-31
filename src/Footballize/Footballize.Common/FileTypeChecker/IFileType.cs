namespace Footballize.Common.FileTypeChecker
{
    using System.IO;

    public interface IFileType
    {
        string Extension { get; }
        string Name { get; }

        bool Matches(Stream stream);
    }
}