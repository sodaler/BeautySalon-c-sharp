using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.BindingModels
{
    public class EstimateBindingModel
    {
        public int? Id { get; set; }
        public int EstimateRate { get; set; }
        public string Comment { get; set; }
        public int ProcedureId { get; set;}
        public int? ClientId { get; set; }
        public DateTime DateEstimate { get; set; }
    }
}
