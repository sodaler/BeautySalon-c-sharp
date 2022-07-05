using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.ViewModels
{
    //Модель для получения списка услуг по выбранным заказам (роль Работник)
    public class ReportOrderServiceViewModel
    {
        public string OrderName { get; set; }
        public List<string> Services { get; set; }
    }
}
