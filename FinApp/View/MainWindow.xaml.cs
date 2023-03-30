
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
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataManageViewModel();
        }
    }
}
