using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.ViewModels
{
    public class ProcedureViewModel
    {
        public int Id { get; set; }

        [DisplayName("Наименование процедуры")]
        public string ProcedureName { get; set; }

        [DisplayName("Процентная ставка")]
        public decimal ProcedurePrice { get; set; }
        public Dictionary<int, string> ProcedureOrders { get; set; }
        public int? ClientId { get; set; }
    }
}
