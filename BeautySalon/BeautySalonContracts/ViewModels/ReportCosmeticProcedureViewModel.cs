using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.ViewModels
{
    //Модель для получения списка процедур по выбранным косметикам (роль Кладовщик)
    public class ReportCosmeticProcedureViewModel
    {
        public string CosmeticName { get; set; }
        public List<Tuple<string, decimal>> Procedures { get; set; }
    }
}
