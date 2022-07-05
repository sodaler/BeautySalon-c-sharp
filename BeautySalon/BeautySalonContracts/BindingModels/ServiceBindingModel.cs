using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.BindingModels
{
    public class ServiceBindingModel
    {
        public int? Id { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
        public DateTime DateAdding { get; set; }
        public Dictionary<int, string>? ServiceProcedures { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
