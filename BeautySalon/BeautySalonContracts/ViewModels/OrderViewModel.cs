using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string OrderName { get; set; }

        [DisplayName("Цена")]
        public int Price { get; set; }

        [DisplayName("Дата создания")]
        public DateTime CreateDate { get; set; }
        public Dictionary<int, string> OrderCosmetics { get; set; }
    }
}
