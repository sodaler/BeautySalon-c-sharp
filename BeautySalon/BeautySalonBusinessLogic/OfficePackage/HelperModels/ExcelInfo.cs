using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautySalonContracts.ViewModels;

namespace BeautySalonBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportOrderServiceViewModel> OrderService { get; set; }
        public List<ReportCosmeticProcedureViewModel> CosmeticProcedure { get; set; }
    }
}
