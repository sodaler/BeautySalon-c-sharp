﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankContracts.BindingModels;
using BankContracts.BusinessLogicsContracts;
using Unity;

namespace BankView
{
    public partial class FormDeposits : Form
    {
        public IDepositLogic _logic;
        public FormDeposits(IDepositLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }
        private void FormDeposits_Load(object sender, EventArgs e)
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
                    dataGridView.Rows.Clear();
                    foreach (var deposit in list)
                    {
                        string stringClients = string.Empty;
                        foreach (var client in deposit.DepositOrders)
                        {
                            stringClients += client.Key + ") " + client.Value + ", ";
                        }
                        dataGridView.Rows.Add(new object[] { deposit.Id, deposit.DepositName, deposit.DepositInterest, stringClients });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormDeposit>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormDeposit>();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        _logic.Delete(new DepositBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }       
    }
}
