using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [DisplayName("ФИО")]
        public string ManagerFIO { get; set; }

        [DisplayName("Логин")]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
