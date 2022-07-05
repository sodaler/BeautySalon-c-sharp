using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonDatabaseImplement.Models
{
    public class ProcedureService
    {
        public int Id { get; set; }
        public int ProcedureId { get; set; }
        public int ServiceId { get; set; }
        public virtual Procedure Procedure { get; set; }
        public virtual Service Service { get; set; }
    }
}
