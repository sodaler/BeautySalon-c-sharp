using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.ViewModels
{
    public class LaborCostViewModel
    {
        public int Id { get; set; }
        public int CosmeticId { get; set; }

        [DisplayName("Начало даты")]
        public DateTime StartLaborCost { get; set; }

        [DisplayName("Окончание даты")]
        public DateTime EndLaborCost { get; set; }
        [DisplayName("Косметика")]
        public string CosmeticName { get; set; }

    }
}
