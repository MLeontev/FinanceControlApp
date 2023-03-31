
using System.Windows;
using System.Windows.Controls;
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
        public static ListView AllAccountsView;
        public static ListView AllCategoriesView;
        public static ListView AllOperationsView;

        public MainWindow()
        {
            InitializeComponent();
            db.Database.EnsureCreated();
            DataContext = new DataManageViewModel();

            AllAccountsView = ViewAllAccounts;
            AllCategoriesView = ViewAllCategories;
            AllOperationsView = ViewAllOperations;
        }
    }
}
