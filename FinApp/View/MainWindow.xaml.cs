
using System.Windows;
using FinApp.Model.Data;
using FinApp.ViewModel;

namespace FinApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db = new ApplicationContext();
        public MainWindow()
        {
            InitializeComponent();
            db.Database.EnsureCreated();
            DataContext = new DataManageViewModel();
        }
    }
}
