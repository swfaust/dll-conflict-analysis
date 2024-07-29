using System.IO;
using RDES.ConflictAnalyzer.DataModel;

namespace RDES.ConflictAnalyzer.Services.Implementation;

public class FileService : IFileService
{
    public List<FileData> GetFilesFromFolder(string rootFolder, bool recursive, string searchPattern = "*.dll")
    {
        if (!Directory.Exists(rootFolder))
            throw new InvalidDataException($"Directory {rootFolder} does not exist");

        return Directory.GetFiles(rootFolder, searchPattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
            .Select(FileData.FromPath)
            .ToList();
    }
}