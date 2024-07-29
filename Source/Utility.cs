using RDES.ConflictAnalyzer.DataModel;

namespace RDES.ConflictAnalyzer;

public static class Utility
{
    public static List<FileComparison> CompareFileLists(List<FileData> filesA, List<FileData> filesB)
    {
        var comparisons = new List<FileComparison>();

        var uniqueList = filesA.Concat(filesB).OrderBy(x => x.FileName).Distinct().ToList();

        foreach (var fileData in uniqueList)
        {
            var fileA = filesA.FirstOrDefault(x => x.FileName == fileData.FileName);
            var fileB = filesB.FirstOrDefault(x => x.FileName == fileData.FileName);

            comparisons.Add(new FileComparison(fileA, fileB));
        }

        return comparisons;
    }
}