using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonDatabaseImplement.Models
{
    public class Estimate
    {
        public int Id { get; set; }

        [Required]
        public int EstimateRate { get; set; }
        public string Comment { get; set; }


        [Required]
        public DateTime DateEstimate { get; set; }
        public int ProcedureId { get; set; }
        public virtual Procedure Procedure { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
