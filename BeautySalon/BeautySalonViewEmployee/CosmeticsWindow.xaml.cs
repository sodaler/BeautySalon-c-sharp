﻿using BeautySalonContracts.BindingModels;
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
using Unity;

namespace BeautySalonViewEmployee
{
    /// <summary>
    /// Логика взаимодействия для CosmeticsWindow.xaml
    /// </summary>
    public partial class CosmeticsWindow : Window
    {
        private readonly ICosmeticLogic _logic;

        public CosmeticsWindow(ICosmeticLogic logic)
        {
            InitializeComponent();
            _logic = logic;

        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                var form = App.Container.Resolve<CosmeticWindow>();
                form.Id = Convert.ToInt32(((CosmeticViewModel)dataGrid.SelectedItem).Id);
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }


        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = _logic.Read(null);
                if (list != null)
                {
                    dataGrid.ItemsSource=list;
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
            LoadData();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<CosmeticWindow>();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                if (MessageBox.Show("Удалить?", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id =
                   Convert.ToInt32(((CosmeticViewModel)dataGrid.SelectedItem).Id);
                    try
                    {
                        _logic.Delete(new CosmeticBindingModel { Id = id });
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
    }
}
