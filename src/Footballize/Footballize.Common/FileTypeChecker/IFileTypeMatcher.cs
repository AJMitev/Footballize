namespace Footballize.Common.FileTypeChecker
{
    using System.IO;

    public interface IFileTypeMatcher
    {
        bool Matches(Stream stream, bool resetPosition = true);
    }
}