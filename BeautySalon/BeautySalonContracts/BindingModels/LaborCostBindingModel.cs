using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.BindingModels
{
    public class LaborCostBindingModel
    {
        public int? Id { get; set; }
        public int? CosmeticId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime StartLaborCost { get; set; }
        public DateTime EndLaborCost { get; set; }
    }
}
