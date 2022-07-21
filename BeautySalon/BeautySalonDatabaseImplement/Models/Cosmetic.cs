using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonDatabaseImplement.Models
{
    public class Cosmetic
    {
        public int Id { get; set; }

        [Required]
        public string CosmeticName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("CosmeticId")]
        public virtual List<OrderCosmetic> OrderCosmetics { get; set; }

        [ForeignKey("CosmeticId")]
        public virtual List<CosmeticService> CosmeticServices { get; set; }

        [ForeignKey("CosmeticId")]
        public virtual List<LaborCost> LaborCosts { get; set; }
        public int ManagerId { get; set; }
        public virtual Employee Manager { get; set; }

    }
}
