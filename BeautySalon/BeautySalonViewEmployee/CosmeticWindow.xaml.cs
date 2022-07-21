using BeautySalonContracts.BindingModels;
using BeautySalonContracts.BusinessLogicsContracts;
using BeautySalonContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
using Unity;

namespace BeautySalonViewEmployee
{
    /// <summary>
    /// Логика взаимодействия для CosmeticsWindow.xaml
    /// </summary>
    public partial class CosmeticWindow : Window
    {
        public int Id { set { id = value; } }
        private readonly ICosmeticLogic _logic;
        private int? id;
        private Dictionary<int, (string, decimal)> cosmeticServices;

        public CosmeticWindow(ICosmeticLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            
            if (dataGrid.SelectedItems.Count == 1)
            {
                var form = App.Container.Resolve<CosmeticServiceWindow>();
                PropertyInfo prop = dataGrid.SelectedItem.GetType().GetProperty("Item1");
                int id = Convert.ToInt32(prop.GetValue(dataGrid.SelectedItem, Array.Empty<object>()));
                form.Id = id;

                if (form.ShowDialog() == true)
                {
                    cosmeticServices.Remove(id);
                    cosmeticServices[form.Id] = (form.ServiceName, form.ServicePrice);
                    LoadData();
                }
            }
            
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        PropertyInfo prop = dataGrid.SelectedItem.GetType().GetProperty("Item1");
                        cosmeticServices.Remove(Convert.ToInt32(prop.GetValue(dataGrid.SelectedItem, Array.Empty<object>())));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxRate.Text))
            {
                MessageBox.Show("Заполните процентную ставку", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (cosmeticServices == null || cosmeticServices.Count == 0)
            {
                MessageBox.Show("Заполните услуги", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new CosmeticBindingModel
                {
                    Id = id,
                    CosmeticName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxRate.Text),
                    CosmeticServices = cosmeticServices,
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

        private void LoadData()
        {
           
            try
            {
                if (cosmeticServices != null)
                {
                    var services = cosmeticServices.Select(rec=>(rec.Key, rec.Value.Item1,rec.Value.Item2).ToTuple());
                    dataGrid.ItemsSource = services;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CosmeticViewModel view = _logic.Read(new CosmeticBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.CosmeticName;
                        textBoxRate.Text = view.Price.ToString();
                        cosmeticServices = view.CosmeticServices;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                }
            }
            else
            {
                cosmeticServices = new Dictionary<int, (string, decimal)>();
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            
           var form = App.Container.Resolve<CosmeticServiceWindow>();
           if (form.ShowDialog() == true)
           {
               if (cosmeticServices.ContainsKey(form.Id))
               {
                   cosmeticServices[form.Id] = (form.ServiceName, form.ServicePrice);
               }
               else
               {
                   cosmeticServices.Add(form.Id, (form.ServiceName, form.ServicePrice));
               }
               LoadData();
           }
           
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

    }
}
