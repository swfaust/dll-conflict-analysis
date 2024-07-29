using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace RDES.ConflictAnalyzer.DataModel;

[DebuggerDisplay("{" + nameof(FileName) + "}")]
public class FileData: NotifyBase
{
    public string FullPath { get; }

    public string FileName { get; }

    public Version Version { get; }

    FileData(string path, Version version)
    {
        FullPath = path;
        FileName = Path.GetFileName(path);
        Version = version;
    }

    public static FileData FromPath(string path)
    {
        if (!File.Exists(path))
            throw new InvalidDataException($"Path {path} does not exist");

        try
        {
            var assembly = Assembly.LoadFile(path);

            var version = assembly.GetName().Version;

            if (version == null)
                throw new InvalidDataException($"Version was null for file {path}");

            return new FileData(path, version);
        }
        catch (BadImageFormatException ex)
        {
            return new FileData(path, new Version());
        }
    }
}