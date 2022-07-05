using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.ViewModels
{
    //Модель для получения отчёта по заказам (роль Работник)
    public class ReportOrdersViewModel
    {
        public string OrderName { get; set; }
        public DateTime CreateDate { get; set; }
        public List<(ProcedureViewModel, List<ServiceViewModel>)> ProcedureServices { get; set;}
    }
}
