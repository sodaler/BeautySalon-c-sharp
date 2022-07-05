using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonDatabaseImplement.Models
{
    public class OrderCosmetic
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CosmeticId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Cosmetic Cosmetic { get; set; }
    }
}
