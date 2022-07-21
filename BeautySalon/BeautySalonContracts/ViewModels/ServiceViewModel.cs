using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.ViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название услуги")]
        public string ServiceName { get; set; }

        [DisplayName("Курс в рублях")]
        public decimal ServicePrice { get; set; }

        [DisplayName("Дата добавления")]
        public DateTime DateAdding { get; set; }
        [DisplayName("Процедуры")]
        public Dictionary<int, (string, decimal)> ServiceProcedures { get; set; }
        public string PrettyProcedures { 
            get
            {
                string stringProcedures = string.Empty;
                if (ServiceProcedures != null)
                {
                    stringProcedures = string.Join("; ", ServiceProcedures.Select(dep => dep.Value.Item1 + ": " + dep.Value.Item2));
                    
                }
                return stringProcedures;
            }
            set { }
        }
        
    }
}
