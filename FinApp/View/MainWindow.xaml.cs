
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using FinApp.Model;
using FinApp.Model.Data;
using FinApp.ViewModel;
using MaterialDesignColors.Recommended;
using ScottPlot;

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
        public static WpfPlot Chart;
        private static double[] values;
        private static string[] labels;
        

        public MainWindow()
        {
            InitializeComponent();
            db.Database.EnsureCreated();
            DataContext = new DataManageViewModel();

            AllAccountsView = ViewAllAccounts;
            AllCategoriesView = ViewAllCategories;
            AllOperationsView = ViewAllOperations;
            Chart = ExpensePieChart;

            UpdateExpenseChart();
        }

        public static void UpdateExpenseChart()
        {
            Chart.Plot.Clear();

            List<Category> Categories = DataWorker.GetAllCategories();

            values = new double[Categories.Count];
            for (int i = 0; i < Categories.Count; i++)
            {
                int SumAmount = 0;
                List<Operation> ExpensesByCategory = DataWorker.GetAllExpensesByCategoryId(Categories[i].Id);
                foreach (Operation expense in ExpensesByCategory)
                {
                    SumAmount += expense.Amount;
                }
                values[i] = (double)SumAmount;
            }

            labels = new string[Categories.Count];
            for (int i = 0; i < Categories.Count; i++)
            {
                int SumAmount = 0;
                List<Operation> ExpensesByCategory = DataWorker.GetAllExpensesByCategoryId(Categories[i].Id);
                foreach (Operation expense in ExpensesByCategory)
                {
                    SumAmount += expense.Amount;
                }

                labels[i] = Categories[i].Name + " " + Convert.ToString(SumAmount) + "₽";
            }

            var pie = Chart.Plot.AddPie(values);
            pie.DonutSize = .6;
            pie.DonutLabel = "Расходы";
            pie.SliceLabels = labels;
            pie.OutlineSize = 1;
            Chart.Plot.Legend();

            Chart.Plot.SaveFig("pie_showEverything.png");

            Chart.Refresh();
        }
    }
}
