using BeautySalonBusinessLogic.Mail;
using BeautySalonContracts.BindingModels;
using BeautySalonContracts.BusinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для SendMailWindow.xaml
    /// </summary>
    public partial class SendMailWindow : Window
    {
        private readonly IReportLogic _reportLogic;
        private readonly MailKitWorker _mailKitWorker;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public SendMailWindow(IReportLogic reportLogic, MailKitWorker mailKitWorker)
        {
            InitializeComponent();
            _reportLogic = reportLogic;
            _mailKitWorker = mailKitWorker;
        }

        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            
            if (!Regex.IsMatch(textBoxEmail.Text, @"([a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z0-9]+)"))
            {
                textBoxEmail.Focus();
                MessageBox.Show("Введеный текст не является адресом электронной почты", "Ошибка", MessageBoxButton.OK,
              MessageBoxImage.Error);
                return;
            }
            try
            {
                _reportLogic.SaveServicesToPdfFile(new ReportBindingModel()
                {
                    DateFrom = DateFrom,
                    DateTo = DateTo,
                    FileName = "услуги.pdf",
                    EmployeeId = App.Employee.Id
                });
                var appSettings = ConfigurationManager.AppSettings;
                _mailKitWorker.MailConfig(new MailConfigBindingModel
                {
                    SmtpClientHost = appSettings["SmtpClientHost"],
                    SmtpClientPort = Convert.ToInt32(appSettings["SmtpClientPort"]),
                    PopHost = appSettings["PopHost"],
                    PopPort = Convert.ToInt32(appSettings["PopPort"]),
                    MailLogin = appSettings["MailLogin"],
                    MailPassword = appSettings["MailPassword"]
                });
                _mailKitWorker.MailSendAsync(new MailSendInfoBindingModel
                {
                    MailAddress = textBoxEmail.Text,
                    Subject = "Отчет по услугам. Салон красоты \"Вы ужасны\"",
                    Text = "Отчет по услугам с " + DateFrom.ToShortDateString() + " по " + DateTo.ToShortDateString() +
                    "\nСотрудник - " + App.Employee.Id,
                    FileName = "услуги.pdf",
                });
                MessageBox.Show("Письмо успешно отправлено");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
