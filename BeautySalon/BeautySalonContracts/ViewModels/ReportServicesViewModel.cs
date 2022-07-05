using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.ViewModels
{
    //Модель для получения отчёта по услугам (роль Кладовщик)
    public class ReportServicesViewModel
    {
        public string ServiceName { get; set; }
        public DateTime DateAdding { get; set; }
        public string Procedures { get; set; }
        public string Cosmetics { get; set; }
    }
}
