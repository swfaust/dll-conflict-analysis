using RDES.ConflictAnalyzer.Services.Implementation;
using RDES.ConflictAnalyzer.ViewModel;

namespace RDES.ConflictAnalyzer.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Wnd_Main
    {
        public Wnd_Main()
        {
            InitializeComponent();

            DataContext = new MainVM(new FileService());
        }
    }
}