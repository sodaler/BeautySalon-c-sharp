using BeautySalonContracts.BindingModels;
using BeautySalonContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BeautySalonClientApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IWebHostEnvironment _environment;

        public ReportController(ILogger<ReportController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public IActionResult ReportWordExcel()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Orders = APIClient.GetRequest<List<OrderViewModel>>($"api/client/GetClientOrderList?clientId={Program.Client.Id}");
            return View();
        }

        [HttpPost]
        public IActionResult CreateReportOrderServiceToWordFile(List<int> ordersId)
        {
            if (ordersId != null)
            {
                var model = new ReportBindingModel
                {
                    Orders = new List<OrderViewModel>(),
                    Cosmetics = new List<CosmeticViewModel>()
                };
                foreach (var orderId in ordersId)
                {
                    model.Orders.Add(APIClient.GetRequest<OrderViewModel>($"api/order/GetOrder?orderId={orderId}"));
                }
                model.FileName = @"..\BankClientApp\wwwroot\ReportOrderService\ReportOrderServiceDoc.doc";
                APIClient.PostRequest("api/report/CreateReportOrderServiceToWordFile", model);
                var fileName = "ReportOrderServiceDoc.doc";
                var filePath = _environment.WebRootPath + @"\ReportOrderService\" + fileName;
                return Redirect("ReportWordExcel");
                //Response.Redirect(PhysicalFile(filePath, "application/doc", fileName));
            }
            throw new Exception("Выберите хотя бы одного заказа");
        }

        [HttpPost]
        public IActionResult CreateReportOrderServiceToExcelFile(List<int> ordersId)
        {
            if (ordersId != null)
            {
                var model = new ReportBindingModel
                {
                    Orders = new List<OrderViewModel>(),
                    Cosmetics = new List<CosmeticViewModel>()
                };
                foreach (var orderId in ordersId)
                {
                    model.Orders.Add(APIClient.GetRequest<OrderViewModel>($"api/order/GetOrder?orderId={orderId}"));
                }
                model.FileName = @"..\BankClientApp\wwwroot\ReportOrderService\ReportOrderServiceExcel.xls";
                APIClient.PostRequest("api/report/CreateReportOrderServiceToExcelFile", model);
                var fileName = "ReportOrderServiceExcel.xls";
                var filePath = _environment.WebRootPath + @"\ReportOrderService\" + fileName;
                return PhysicalFile(filePath, "application/xls", fileName);
            }
            throw new Exception("Выберите хотя бы одного заказа");
        }

        public IActionResult ReportPDF()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ReportGetOrdersPDF(DateTime dateFrom, DateTime dateTo)
        {
            ViewBag.Period = "C " + dateFrom.ToLongDateString() + " по " + dateTo.ToLongDateString();
            ViewBag.Report = APIClient.GetRequest<List<ReportOrdersViewModel>>($"api/report/GetOrdersReport?dateFrom={dateFrom.ToLongDateString()}&dateTo={dateTo.ToLongDateString()}");
            return View("ReportPdf");
        }

        [HttpPost]
        public IActionResult SendReportOnMail()
        {
            return View();
        }
    }
}
