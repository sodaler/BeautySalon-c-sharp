using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautySalonContracts.ViewModels;

namespace BeautySalonContracts.BindingModels
{
    public class ReportBindingModel
    {
        public string FileName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<OrderViewModel>? Orders { get; set; }
        public List<CosmeticViewModel>? Cosmetics { get; set; }
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
    }
}
