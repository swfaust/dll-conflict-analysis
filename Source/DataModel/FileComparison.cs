namespace RDES.ConflictAnalyzer.DataModel;

public class FileComparison
{
    public FileData? FileA { get; }

    public FileData? FileB { get; }

    public bool HasBothFiles => FileA != null && FileB != null;

    public bool HasVersionMatch => FileA?.Version == FileB?.Version;

    public bool HasVersionMisMatch => !HasVersionMatch;

    public FileComparison(FileData? fileA, FileData? fileB)
    {
        FileA = fileA;
        FileB = fileB;
    }
}