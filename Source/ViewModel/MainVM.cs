using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Win32;
using RDES.ConflictAnalyzer.DataModel;
using RDES.ConflictAnalyzer.Services;

namespace RDES.ConflictAnalyzer.ViewModel;

public class MainVM : ClosableDialogBase
{
    readonly IFileService _fileService;
    readonly CollectionViewSource _comparisonSource;

    string? _rootFolderA;
    string? _rootFolderB;
    string _filePattern;
    bool _recursive;
    Enums.ViewOptions _viewOption;

    public string? RootFolderA
    {
        get => _rootFolderA;
        set
        {
            if (value == _rootFolderA) return;
            _rootFolderA = value;
            OnPropertyChanged();
        }
    }

    public string? RootFolderB
    {
        get => _rootFolderB;
        set
        {
            if (value == _rootFolderB) return;
            _rootFolderB = value;
            OnPropertyChanged();
        }
    }

    public string FilePattern
    {
        get => _filePattern;
        set
        {
            if (value == _filePattern) return;
            _filePattern = value;
            OnPropertyChanged();
        }
    }

    public bool Recursive
    {
        get => _recursive;
        set
        {
            if (value == _recursive) return;
            _recursive = value;
            OnPropertyChanged();
        }
    }

    public Enums.ViewOptions ViewOption
    {
        get => _viewOption;
        set
        {
            if (value == _viewOption) return;
            _viewOption = value;
            OnPropertyChanged();
            ComparisonsView.Refresh();
        }
    }

    public List<Enums.ViewOptions> AllViewOptions => 
        Enum.GetValues<Enums.ViewOptions>().ToList();

    public ObservableCollection<FileComparison> Comparisons { get; }

    public ICollectionView ComparisonsView => _comparisonSource.View;

    public ICommand CmdBrowseA { get; }

    public ICommand CmdBrowseB { get; }

    public ICommand CmdCompare { get; }

    public MainVM(IFileService fileService)
    {
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        Comparisons = new ObservableCollection<FileComparison>();

        CmdBrowseA = new RelayCommand(_ => Browse(true));
        CmdBrowseB = new RelayCommand(_ => Browse(false));
        CmdCompare = new RelayCommand(_ => Compare(),
            _ => Directory.Exists(RootFolderA) && Directory.Exists(RootFolderB));

        _viewOption = Enums.ViewOptions.All;

        _comparisonSource = new CollectionViewSource() { Source = Comparisons };
        ComparisonsView.Filter = x =>
        {
            if (!(x is FileComparison comparison))
                throw new InvalidDataException("Item was the incorrect type");

            switch (ViewOption)
            {
                case Enums.ViewOptions.All:
                    return true;
                case Enums.ViewOptions.Matching:
                    return comparison.HasBothFiles;
                case Enums.ViewOptions.Problems:
                    return comparison is { HasBothFiles: true, HasVersionMisMatch: true };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        };

        _filePattern = "*.dll";
    }

    void Browse(bool isA)
    {
        var dlg = new OpenFolderDialog();
        if (dlg.ShowDialog() != true)
            return;

        if (isA)
            RootFolderA = dlg.FolderName;
        else
            RootFolderB = dlg.FolderName;
    }

    void Compare()
    {
        if (string.IsNullOrWhiteSpace(RootFolderA) || string.IsNullOrWhiteSpace(RootFolderB))
            return;

        Comparisons.Clear();

        var filesA = _fileService.GetFilesFromFolder(RootFolderA, Recursive, FilePattern);
        var filesB = _fileService.GetFilesFromFolder(RootFolderB, Recursive, FilePattern);

        Utility.CompareFileLists(filesA, filesB).ForEach(x => Comparisons.Add(x));
    }
}