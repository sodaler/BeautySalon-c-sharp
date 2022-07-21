using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.BindingModels
{
    public class OrderBindingModel
    {
        public int? Id { get; set; }
        public string OrderName { get; set; }
        public int Price {get; set;}
        public DateTime DateVisit { get; set; }
        public Dictionary<int, string> OrderCosmetics { get; set; }
        public int? ClientId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
