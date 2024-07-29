using RDES.ConflictAnalyzer.DataModel;

namespace RDES.ConflictAnalyzer;

public class ComparerFileDataNameOnly : IEqualityComparer<FileData>
{
    public bool Equals(FileData? x, FileData? y)
    {
        //if (ReferenceEquals(x, y)) return true;
        //if (ReferenceEquals(x, null)) return false;
        //if (ReferenceEquals(y, null)) return false;
        //if (x.GetType() != y.GetType()) return false;
        return x?.FileName == y?.FileName;
    }

    public int GetHashCode(FileData obj)
    {
        return HashCode.Combine(obj.FileName);
    }
}