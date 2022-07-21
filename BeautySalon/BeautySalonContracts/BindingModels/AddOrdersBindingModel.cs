using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.BindingModels
{
    public class AddOrdersBindingModel
    {
        public int ProcedureId { get; set; }
        public List<int> OrdersId { get; set; }
    }
}
