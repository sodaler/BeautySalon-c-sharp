using BeautySalonContracts.BindingModels;
using BeautySalonContracts.BusinessLogicsContracts;
using BeautySalonContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportLogic _reportLogic;
        private readonly IOrderLogic _orderLogic;
        public ReportController(IReportLogic reportLogic, IOrderLogic orderLogic)
        {
            _reportLogic = reportLogic;
            _orderLogic = orderLogic;
        }

        [HttpPost]
        public void CreateReportOrderServiceToWordFile(ReportBindingModel model) => _reportLogic.SaveOrderServiceToWordFile(model);

        [HttpPost]
        public void CreateReportOrderServiceToExcelFile(ReportBindingModel model) => _reportLogic.SaveOrderServiceToExcelFile(model);

        [HttpPost]
        public void CreateReportOrdersToPdfFile(ReportBindingModel model) => _reportLogic.SaveOrdersToPdfFile(model);

        [HttpGet]
        public List<ReportOrdersViewModel> GetOrdersReport(string dateFrom, string dateTo) => _reportLogic.GetOrders(new ReportBindingModel { DateFrom = Convert.ToDateTime(dateFrom), DateTo = Convert.ToDateTime(dateTo) });

        [HttpGet]
        public ReportBindingModel GetOrdersForReport(int ClientId)
        {
            return new ReportBindingModel
            {
                ClientId = ClientId,
                Orders = _orderLogic.Read(new OrderBindingModel { ClientId = ClientId })
            };
        }

    }
}
