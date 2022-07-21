using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.ViewModels
{
    public class CosmeticViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название косметики")]
        public string CosmeticName { get; set; }

        [DisplayName("Цена косметики")]
        public decimal Price { get; set; }

        [DisplayName("Услуги")]
        public Dictionary<int, (string, decimal)> CosmeticServices { get; set; }
        public string PrettyServices
        {
            get
            {
                string stringServices = string.Empty;
                if (CosmeticServices != null)
                {
                    stringServices = string.Join("; ", CosmeticServices.Select(dep => dep.Value.Item1 + ": " + dep.Value.Item2));

                }
                return stringServices;
            }
            set { }
        }

    }
}
