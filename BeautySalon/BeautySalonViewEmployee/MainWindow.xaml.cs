using BeautySalonBusinessLogic;
using BeautySalonContracts.BindingModels;
using BeautySalonContracts.BusinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;

namespace BeautySalonViewEmployee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int employeeId { get; set; }
        private readonly IEmployeeLogic _employeeLogic;
        public MainWindow(IEmployeeLogic employeeLogic)
        {
            InitializeComponent();
            _employeeLogic = employeeLogic;
        }

        private void menuItemService_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<ServicesWindow>();
            form.ShowDialog();
        }

        private void menuItemCosmetic_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<CosmeticsWindow>();
            form.ShowDialog();
        }

        private void menuItemLaborCost_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<LaborCostsWindow>();
            form.ShowDialog();
        }

        private void menuItemGetList_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<ProcedureListWindow>();
            form.ShowDialog();
        }

        private void menuItemReport_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<ReportWindow>();
            form.ShowDialog();
        }

        private void menuItemBindService_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<LinkServiceWindow>();
            form.ShowDialog();
        }

        private void menuItemStatistic_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<WindowStatistic>();
          
            form.ShowDialog();
        }
    }
}
