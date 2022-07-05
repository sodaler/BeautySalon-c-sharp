using BeautySalonContracts.BindingModels;
using BeautySalonContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
  
        List<ReportOrderServiceViewModel> GetOrderService(ReportBindingModel model);

       
        void SaveOrderServiceToWordFile(ReportBindingModel model);


        void SaveOrderServiceToExcelFile(ReportBindingModel model);

        List<ReportOrdersViewModel> GetOrders(ReportBindingModel model);

            
        List<ReportCosmeticProcedureViewModel> GetCosmeticProcedure(ReportBindingModel model);

       
        void SaveCosmeticProcedureToWordFile(ReportBindingModel model);


        void SaveCosmeticProcedureToExcelFile(ReportBindingModel model);


        List<ReportServicesViewModel> GetServices(ReportBindingModel model);

        void SaveServicesToPdfFile(ReportBindingModel model);
        void SaveOrdersToPdfFile(ReportBindingModel model);
    }
}
