using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonDatabaseImplement.Models
{
    public class CosmeticService
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int CosmeticId { get; set; }
        public virtual Service Service { get; set; }
        public virtual Cosmetic Cosmetic { get; set; }
    }
}
