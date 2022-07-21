﻿using BankContracts.BusinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankContracts.BindingModels;
using BankContracts.ViewModels;

namespace BankView
{
    public partial class FormReportClientCurrency : Form
    {
        private readonly IOrderLogic _logicC;
        private readonly IReportLogic _logicR;
        public FormReportClientCurrency(IOrderLogic logicC, IReportLogic logicR)
        {
            InitializeComponent();
            _logicC = logicC;
            _logicR = logicR;
        }
        private void FormReportClientCurrency_Load(object sender, EventArgs e)
        {
            try
            {
                var list = _logicC.Read(null);
                if (list != null)
                {                    
                    checkedListBoxClients.DataSource = list;
                    checkedListBoxClients.DisplayMember = "ClientFIO";
                    checkedListBoxClients.ValueMember = "Id";
                    checkedListBoxClients.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonWord_Click(object sender, EventArgs e)
        {
            var selectedClients = new List<OrderViewModel>();
            foreach (var client in checkedListBoxClients.CheckedItems)
            {
                selectedClients.Add((OrderViewModel)client);
            }
            /*_logicR.GetClientCurrency(new ReportBindingModel
            {
                Clients = selectedClients
            });*/
            using var dialog = new SaveFileDialog { Filter = "docx|*.docx" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _logicR.SaveOrderCurrencyToWordFile(new ReportBindingModel
                {
                    FileName = dialog.FileName,
                    Orders = selectedClients
                });
                MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
        }

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            var selectedClients = new List<OrderViewModel>();
            foreach (var client in checkedListBoxClients.CheckedItems)
            {
                selectedClients.Add((OrderViewModel)client);
            }
            using var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _logicR.SaveOrderCurrencyToExcelFile(new ReportBindingModel
                    {
                        FileName = dialog.FileName,
                        Orders = selectedClients
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }        
    }
}
