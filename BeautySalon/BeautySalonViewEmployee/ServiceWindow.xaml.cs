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
using System.Windows.Shapes;

namespace BeautySalonViewEmployee
{
    /// <summary>
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        public int Id { set { id = value; } }
        private readonly IServiceLogic _logic;
        private int? id;
        public ServiceWindow(IServiceLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxRate.Text))
            {
                MessageBox.Show("Заполните курс", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new ServiceBindingModel
                {
                    Id = id,
                    ServiceName = textBoxName.Text,
                    ServicePrice = Convert.ToDecimal(textBoxRate.Text),
                    DateAdding = DateTime.Now,
                    EmployeeId = App.Employee.Id
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = _logic.Read(new ServiceBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.ServiceName;
                        textBoxRate.Text = view.ServicePrice.ToString();
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
}
