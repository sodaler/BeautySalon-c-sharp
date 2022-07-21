using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonDatabaseImplement.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        public string ServiceName { get; set; }

        [Required]
        public decimal ServicePrice { get; set; }

        [Required]
        public DateTime DateAdding { get; set; }

        [ForeignKey("ServiceId")]
        public virtual List<CosmeticService> CosmeticServices { get; set; }

        [ForeignKey("ServiceId")]
        public virtual List<ProcedureService> ProcedureServices { get; set; }
        public int ManagerId { get; set; }
        public virtual Employee Manager { get; set; }
    }
}
