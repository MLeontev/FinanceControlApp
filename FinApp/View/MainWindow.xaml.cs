using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using FinApp.Model;
using FinApp.Model.Data;
using FinApp.ViewModel;
using MaterialDesignColors.Recommended;
using ScottPlot;
using ScottPlot.Plottable;
using ScottPlot.WPF;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;
using ApplicationContext = FinApp.Model.Data.ApplicationContext;

namespace FinApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db = new ApplicationContext();
        public static System.Windows.Controls.ListView AllAccountsView;
        public static System.Windows.Controls.ListView AllCategoriesView;
        public static System.Windows.Controls.ListView AllOperationsView;
        public static WpfPlot IncomeChart;
        public static WpfPlot ExpenseChart;
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
            IncomeChart = IncomePieChart;
            ExpenseChart = ExpensePieChart;

            UpdateIncomeChart();
            UpdateExpenseChart();
        }

        public static void UpdateIncomeChart()
        {
            IncomeChart.Plot.Clear();

            List<Category> Categories = DataWorker.GetAllCategories();

            values = new double[Categories.Count];
            for (int i = 0; i < Categories.Count; i++)
            {
                int SumAmount = 0;
                List<Operation> IncomesByCategory = DataWorker.GetAllIncomesByCategoryId(Categories[i].Id);
                foreach (Operation expense in IncomesByCategory)
                {
                    SumAmount += expense.Amount;
                }
                values[i] = (double)SumAmount;
            }

            labels = new string[Categories.Count];
            for (int i = 0; i < Categories.Count; i++)
            {
                int SumAmount = 0;
                List<Operation> IncomesByCategory = DataWorker.GetAllIncomesByCategoryId(Categories[i].Id);
                foreach (Operation expense in IncomesByCategory)
                {
                    SumAmount += expense.Amount;
                }

                labels[i] = Categories[i].Name + " " + Convert.ToString(SumAmount) + "₽";
            }

            if (labels.Length != 0 && labels.Length != 0)
            {
                var pie = IncomeChart.Plot.AddPie(values);
                pie.DonutSize = .6;
                pie.SliceLabels = labels;
                pie.OutlineSize = 1;
                IncomeChart.Plot.Legend();
                IncomeChart.Visibility = Visibility.Visible;

                IncomeChart.Plot.SaveFig("pie_showEverything.png");
            }
            else
            {
                IncomeChart.Visibility = Visibility.Collapsed;
            }

            IncomeChart.Refresh();
        }

        public static void UpdateExpenseChart()
        {
            ExpenseChart.Plot.Clear();

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

            if (labels.Length != 0 && labels.Length != 0)
            {
                var pie = ExpenseChart.Plot.AddPie(values);
                pie.DonutSize = .6;
                pie.SliceLabels = labels;
                pie.OutlineSize = 1;
                ExpenseChart.Plot.Legend();
                ExpenseChart.Visibility = Visibility.Visible;

                ExpenseChart.Plot.SaveFig("pie_showEverything.png");
            }
            else
            {
                ExpenseChart.Visibility = Visibility.Collapsed;
            }

            ExpenseChart.Refresh();
        }
    }
}
