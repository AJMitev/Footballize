namespace Footballize.Common.FileTypeChecker
{
    using System.Collections.Generic;
    using System.IO;

    public interface IFileTypeChecker
    {
        IFileType GetFileType(Stream fileContent);
        IEnumerable<IFileType> GetFileTypes(Stream stream);
    }
}