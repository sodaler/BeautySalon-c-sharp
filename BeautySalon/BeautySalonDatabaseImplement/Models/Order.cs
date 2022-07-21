using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string OrderName { get; set; }

        [Required]
        public int Price { get; set; }

     
        [Required]
        public DateTime CreateDate { get; set; }

        [ForeignKey("OrderId")]
        public virtual List<OrderCosmetic> OrderCosmetics { get; set; }

        [ForeignKey("OrderId")]
        public virtual List<OrderProcedure> OrderProcedures { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
