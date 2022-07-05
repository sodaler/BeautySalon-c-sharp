using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonDatabaseImplement.Models
{
    public class Procedure
    {
        public int Id { get; set; }
        
        [Required]
        public string ProcedureName { get; set; }

        [Required]
        public decimal ProcedurePrice { get; set; }

        [ForeignKey("ProcedureId")]
        public virtual List<OrderProcedure> ProcedureOrders { get; set; }

        [ForeignKey("ProcedureId")]
        public virtual List<ProcedureService> ProcedureServices { get; set; }

        [ForeignKey("ProcedureId")]
        public virtual List<Estimate> Estimates { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
