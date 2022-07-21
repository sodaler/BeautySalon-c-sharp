using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonDatabaseImplement.Models
{
    public class LaborCost
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartLaborCost { get; set; }

        [Required]
        public DateTime EndLaborCost { get; set; }
        public int CosmeticId { get; set; }
        public virtual Cosmetic Cosmetic { get; set; }
        public int ManagerId { get; set; }
        public virtual Employee Manager { get; set; }
    }
}
