using BeautySalonContracts.BindingModels;
using BeautySalonContracts.BusinessLogicsContracts;
using BeautySalonContracts.ViewModels;
using Microsoft.Win32;
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
    /// Логика взаимодействия для ProcedureListWindow.xaml
    /// </summary>
    public partial class ProcedureListWindow : Window
    {
        private readonly ICosmeticLogic _logicLP;
        private readonly IReportLogic _logicR;
        public ProcedureListWindow(ICosmeticLogic logicLP, IReportLogic logicR)
        {
            _logicLP = logicLP;
            _logicR = logicR;
            InitializeComponent();
        }

        private void buttonWord_Click(object sender, RoutedEventArgs e)
        {
            var itemsLP = new List<CosmeticViewModel>();
            foreach (var check in listBox.SelectedItems)
            {
                itemsLP.Add((CosmeticViewModel)check);
            }

            var dialog = new SaveFileDialog { Filter = "docx|*.docx" };
            if (dialog.ShowDialog() == true)
            {
                _logicR.SaveCosmeticProcedureToWordFile(new ReportBindingModel
                {
                    FileName = dialog.FileName,
                    Cosmetics = itemsLP
                });
                MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
        }

        private void buttonExcel_Click(object sender, RoutedEventArgs e)
        { 
            var itemsLP = new List<CosmeticViewModel>();
           
            foreach (var check in listBox.SelectedItems)
            {     
                itemsLP.Add((CosmeticViewModel)check);
            }
            
            var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" };
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _logicR.SaveCosmeticProcedureToExcelFile(new
                    ReportBindingModel
                    {
                        FileName = dialog.FileName,
                        Cosmetics = itemsLP
                    });
                    MessageBox.Show("Выполнено", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

                List<CosmeticViewModel> listLP = _logicLP.Read(null);
                if (listLP != null)
                {
                    listBox.ItemsSource = listLP;
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
