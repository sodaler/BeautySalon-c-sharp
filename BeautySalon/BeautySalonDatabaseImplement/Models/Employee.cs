using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonDatabaseImplement.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string ManagerFIO { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [ForeignKey("ManagerId")]
        public virtual List<LaborCost> LaborCosts { get; set; }

        [ForeignKey("ManagerId")]
        public virtual List<Cosmetic> Cosmetics { get; set; }

        [ForeignKey("ManagerId")]
        public virtual List<Service> Services { get; set; }
    }
}
