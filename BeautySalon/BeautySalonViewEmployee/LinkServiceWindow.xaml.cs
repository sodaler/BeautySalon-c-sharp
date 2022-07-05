using BeautySalonContracts.BindingModels;
using BeautySalonContracts.BusinessLogicsContracts;
using BeautySalonContracts.ViewModels;
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
using System.Windows.Shapes;

namespace BeautySalonViewEmployee
{
    /// <summary>
    /// Логика взаимодействия для LinkServiceWindow.xaml
    /// </summary>
    public partial class LinkServiceWindow : Window
    {
        private readonly IProcedureLogic _logicD;
        private readonly IServiceLogic _logicC;
        private Dictionary<int, string> serviceProcedures;
        public LinkServiceWindow(IProcedureLogic logicD, IServiceLogic logicC)
        {
            InitializeComponent();
            _logicC = logicC;
            _logicD = logicD;
        }

        private void buttonLink_Click(object sender, RoutedEventArgs e)
        {
            ServiceViewModel view = _logicC.Read(new ServiceBindingModel { Id = ((ServiceViewModel)comboBoxService.SelectedItem).Id })?[0];
            serviceProcedures = new Dictionary<int, string>();
            foreach (var dep in listBox.SelectedItems)
            {
                var item = (ProcedureViewModel)dep;
                KeyValuePair<int, string> kvp = new(item.Id, (item.ProcedureName));
                serviceProcedures.Add(kvp.Key, kvp.Value);
            }
            _logicC.CreateOrUpdate(new ServiceBindingModel
            {
                Id = view.Id,
                ServiceName = view.ServiceName,
                DateAdding = view.DateAdding,
                ServicePrice = view.ServicePrice,
                ServiceProcedures = serviceProcedures
            });
            

            if (comboBoxService.SelectedValue == null)
            {
                MessageBox.Show("Выберите услугу", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Привязка прошла успешно", "Сообщение",
               MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            Close();

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                List<ServiceViewModel> listC = _logicC.Read(null);
                if (listC != null)
                {
                    comboBoxService.ItemsSource = listC;
                    comboBoxService.SelectedValuePath = "Id";
                    comboBoxService.DisplayMemberPath = "ServiceName";
                    comboBoxService.SelectedItem = null;
                }

                List<ProcedureViewModel> listD = _logicD.Read(null);
                if (listD != null)
                {
                    listBox.ItemsSource = listD;
                    listBox.SelectedValuePath = "Id";
                    listBox.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
    }
}
