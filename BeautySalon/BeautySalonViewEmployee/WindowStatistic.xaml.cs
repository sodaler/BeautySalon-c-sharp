using BeautySalonContracts.BindingModels;
using BeautySalonContracts.BusinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BeautySalonViewEmployee
{
    /// <summary>
    /// Логика взаимодействия для WindowStatistic.xaml
    /// </summary>
    public partial class WindowStatistic : Window
    {
        
        private readonly IServiceLogic _visitLogic;
        private readonly ICosmeticLogic _cosmeticLogic;
        public WindowStatistic(IServiceLogic visitLogic, ICosmeticLogic cosmeticLogic)
        {
            InitializeComponent();
            _visitLogic = visitLogic;
            _cosmeticLogic = cosmeticLogic;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*var visits = _visitLogic.Read(new ServiceBindingModel { Id = Id++ });
            *//*var ChartInfo = visits*/
                /*.OrderBy(rec => rec.DateAdding)
                .GroupBy(rec => (rec.DateAdding.Year, rec.DateAdding.Month))
                .Select(rec => new Tuple<string, int>(string.Format("{0}.{1}", rec.Key.Month, rec.Key.Year), rec.Count()))
                .ToList();*//*
            List<Tuple<string, int>> ChartInfo = new();
            int count = 0;
            foreach (var visit in visits)
            {
                count++;
                ChartInfo.Add(Tuple.Create(visit.ServiceName, count));
            }*/

            /*var visits = _cosmeticLogic.Read(new CosmeticBindingModel { EmployeeId = App.Employee.Id });
            var animals = _visitLogic.Read(new ServiceBindingModel { EmployeeId = App.Employee.Id });
            List<Tuple<string, int>> ChartInfo = new();
            foreach (var animal in animals)
            {
                int count = 0;
                foreach (var visit in visits)
                {
                    if (visit.Services.Contains(animal.Id))
                        count++;
                }
                ChartInfo.Add(Tuple.Create(animal.ServiceName, count));
            }

            ((PieSeries)VisitsDistribution.Series[0]).ItemsSource = ChartInfo;*/
        }
    }
}
