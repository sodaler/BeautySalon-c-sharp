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
    /// Логика взаимодействия для CosmeticServiceWindow.xaml
    /// </summary>
    public partial class CosmeticServiceWindow : Window
    {
        public int Id
        {
            get { return Convert.ToInt32(comboBoxServices.SelectedValue); }
            set { comboBoxServices.SelectedValue = value; }
        }
        public string ServiceName { get { return comboBoxServices.Text; } }
        public int ServicePrice { get { return (int)((ServiceViewModel)comboBoxServices.SelectedItem).ServicePrice; } }
        public DateTime DateAdding { get { return ((ServiceViewModel)comboBoxServices.SelectedItem).DateAdding; } }
        public CosmeticServiceWindow(IServiceLogic logic)
        {
            InitializeComponent();
            List<ServiceViewModel> list = logic.Read(null);
            if (list != null)
            {
                comboBoxServices.DisplayMemberPath = "ServiceName";
                comboBoxServices.SelectedValuePath = "Id";
                comboBoxServices.ItemsSource = list;
                comboBoxServices.SelectedItem = null;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxServices.SelectedValue == null)
            {
                MessageBox.Show("Выберите услугу", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            DialogResult = true;
            Close();
        }
    }
}
