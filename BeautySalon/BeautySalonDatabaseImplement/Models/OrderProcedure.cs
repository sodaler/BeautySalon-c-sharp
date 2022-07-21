using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonDatabaseImplement.Models
{
    public class OrderProcedure
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProcedureId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Procedure Procedure { get; set; }

    }
}
