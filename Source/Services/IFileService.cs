using RDES.ConflictAnalyzer.DataModel;

namespace RDES.ConflictAnalyzer.Services;

public interface IFileService
{
    List<FileData> GetFilesFromFolder(string rootFolder, bool recursive, string searchPattern = "*.dll");
}