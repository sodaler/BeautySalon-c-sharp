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
    /// Логика взаимодействия для LaborCostWindow.xaml
    /// </summary>
    public partial class LaborCostWindow : Window
    {
        private readonly ILaborCostLogic _logicT;
        private readonly ICosmeticLogic _logicLP;
        public int Id { set { id = value; } }
        private int? id;
        

        public LaborCostWindow(ILaborCostLogic logicT, ICosmeticLogic logicLP)
        {
            InitializeComponent();
            _logicT = logicT;
            _logicLP = logicLP;
            
        }
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(datePickerStart.Text))
            {
                MessageBox.Show("Заполните поле 'Дата начала'", "Ошибка",
               MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(datePickerStart.Text))
            {
                MessageBox.Show("Заполните поле 'Дата конца'", "Ошибка",
               MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxLP.SelectedValue == null)
            {
                MessageBox.Show("Выберите косметику", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logicT.CreateOrUpdate(new LaborCostBindingModel
                {
                    Id = id,
                    StartLaborCost = datePickerStart.DisplayDate,
                    EndLaborCost = datePickerEnd.DisplayDate,
                    CosmeticId = ((CosmeticViewModel)comboBoxLP.SelectedItem).Id,
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

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                List<CosmeticViewModel> list = _logicLP.Read(null);
                if (list != null)
                {

                    comboBoxLP.DisplayMemberPath = "CosmeticName";
                    comboBoxLP.SelectedValuePath = "Id";
                    comboBoxLP.ItemsSource = list;
                    comboBoxLP.SelectedItem = null;
                }
                
                if (id.HasValue)
                {
                    LaborCostViewModel view = _logicT.Read(new LaborCostBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        datePickerStart.SelectedDate = view.StartLaborCost;
                        datePickerEnd.SelectedDate = view.EndLaborCost;
                        var item = _logicLP.Read(new CosmeticBindingModel
                        {
                            Id = view.CosmeticId
                        })?[0];
                        comboBoxLP.SelectedValue = item.Id;
                    }
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
