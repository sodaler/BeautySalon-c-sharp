using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.ViewModels
{
    public class EstimateViewModel
    {
        public int Id { get; set; }

        [DisplayName("Оценка")]
        public int EstimateRate { get; set; }
        [DisplayName("Комментарий")]
        public string Comment { get; set; }

        [DisplayName("Дата выставления оценки")]
        public DateTime DateEstimate { get; set; }
        public int ProcedureId { get; set; }
        public string ProcedureName { get; set; }
    }
}
