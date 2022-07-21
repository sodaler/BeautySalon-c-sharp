using BeautySalonClientApp.Models;
using BeautySalonContracts.BindingModels;
using BeautySalonContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;
using BeautySalonBusinessLogic.Mail;

namespace BeautySalonClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly MailKitWorker _mailKitWorker;


        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment, MailKitWorker mailKitWorker)
        {
            _logger = logger;
            _environment = environment;
            _mailKitWorker = mailKitWorker;
        }

        public IActionResult Index()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
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
                model.FileName = @"..\BeautySalonClientApp\wwwroot\ReportOrderService\ReportOrderServiceDoc.doc";
                APIClient.PostRequest("api/report/CreateReportOrderServiceToWordFile", model);
                var fileName = "ReportOrderServiceDoc.doc";
                var filePath = _environment.WebRootPath + @"\ReportOrderService\" + fileName;
                return PhysicalFile(filePath, "application/doc", fileName);
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
                model.FileName = @"..\BeautySalonClientApp\wwwroot\ReportOrderService\ReportOrderServiceExcel.xls";
                APIClient.PostRequest("api/report/CreateReportOrderServiceToExcelFile", model);
                var fileName = "ReportOrderServiceExcel.xls";
                var filePath = _environment.WebRootPath + @"\ReportOrderService\" + fileName;
                return PhysicalFile(filePath, "application/xls", fileName);
            }
            throw new Exception("Выберите хотя бы один заказ");
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
        public IActionResult SendReportOnMail(DateTime dateFrom, DateTime dateTo)
        {
            var model = new ReportBindingModel
            {
                DateFrom = dateFrom,
                DateTo = dateTo
            };
            model.FileName = @"..\BeautySalonClientApp\wwwroot\ReportOrderService\ReportOrdersPdf.pdf";
            APIClient.PostRequest("api/report/CreateReportOrdersToPdfFile", model);
            _mailKitWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = Program.Client.Email,
                Subject = "Отчет по заказам. Салон красоты \"Вы ужасны\"",
                Text = "Отчет по заказам с " + dateFrom.ToShortDateString() + " по " + dateTo.ToShortDateString() +
                "\nРуководитель - " + Program.Client.ClientFIO,
                FileName = model.FileName,
            });
            return View("ReportPdf");
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Program.Client);
        }

        [HttpPost]
        public void Privacy(string login, string password, string clientFIO)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(clientFIO))
            {
                APIClient.PostRequest("api/client/updatedata", new ClientBindingModel
                {
                    Id = Program.Client.Id,
                    ClientFIO = clientFIO,
                    Email = login,
                    Password = password
                });
                Program.Client.ClientFIO = clientFIO;
                Program.Client.Email = login;
                Program.Client.Password = password;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string login, string password)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                Program.Client = APIClient.GetRequest<ClientViewModel>($"api/client/login?login={login}&password={password}");
                if (Program.Client == null)
                {
                    throw new Exception("Неверный логин/пароль");
                }
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public void Register(string login, string password, string clientFIO)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(clientFIO))
            {
                APIClient.PostRequest("api/client/register", new ClientBindingModel
                {
                    ClientFIO = clientFIO,
                    Email = login,
                    Password = password
                });
                Response.Redirect("Enter");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
        }

        public IActionResult Order()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<OrderViewModel>>($"api/client/GetClientOrderList?clientId={Program.Client.Id}"));
        }

        [HttpGet]
        public IActionResult OrderCreate()
        {
            ViewBag.Cosmetics = APIClient.GetRequest<List<CosmeticViewModel>>("api/order/GetCosmeticList");
            return View();
        }

        [HttpPost]
        public void OrderCreate(string orderName, int price, List<int> cosmeticsId)
        {
            List<CosmeticViewModel> cosmetics = new List<CosmeticViewModel>();
            foreach (var cosmeticId in cosmeticsId)
            {
                cosmetics.Add(APIClient.GetRequest<CosmeticViewModel>($"api/order/GetCosmetic?cosmeticId={cosmeticId}"));
            }            
            if (!string.IsNullOrEmpty(orderName))
            {
                APIClient.PostRequest("api/order/CreateOrUpdateOrder", new OrderBindingModel
                {
                    OrderName = orderName,
                    Price = price,   
                    DateVisit = DateTime.Now,
                    OrderCosmetics = cosmetics.ToDictionary(x => x.Id, x => x.CosmeticName),
                    ClientId = Program.Client.Id
                });
                Response.Redirect("Order");
                return;
            }
            throw new Exception("Введите название");
        }

        [HttpGet]
        public IActionResult OrderUpdate(int orderId)
        {
            ViewBag.Order = APIClient.GetRequest<OrderViewModel>($"api/order/GetOrder?orderId={orderId}");
            ViewBag.Cosmetics = APIClient.GetRequest<List<CosmeticViewModel>>("api/order/GetCosmeticList");
            var Procedures = APIClient.GetRequest<List<ProcedureViewModel>>("api/procedure/GetProcedureList");
            var orderProcedures = new List<ProcedureViewModel>();
            foreach (var dep in Procedures)
            {
                if (dep.ProcedureOrders.ContainsKey(orderId))
                {
                    orderProcedures.Add(dep);
                }
            }
            ViewBag.OrderProcedures = orderProcedures;
            return View();
        }

        [HttpPost]
        public void OrderUpdate(int orderId, string orderName, int price, List<int> cosmeticsId)
        {
            if (!string.IsNullOrEmpty(orderName) && price != 0)
            {
                var order = APIClient.GetRequest<OrderViewModel>($"api/order/GetOrder?orderId={orderId}");
                if (order == null)
                {
                    return;
                }
                List<CosmeticViewModel> cosmetics = new List<CosmeticViewModel>();
                foreach (var cosmeticId in cosmeticsId)
                {
                    cosmetics.Add(APIClient.GetRequest<CosmeticViewModel>($"api/order/GetCosmetic?cosmeticId={cosmeticId}"));
                }
                APIClient.PostRequest("api/order/CreateOrUpdateOrder", new OrderBindingModel
                {
                    Id = order.Id,
                    OrderName = orderName,
                    Price = price,
                    DateVisit = DateTime.Now,
                    OrderCosmetics = cosmetics.ToDictionary(x => x.Id, x => x.CosmeticName),
                    ClientId = Program.Client.Id
                });
                Response.Redirect("Order");
                return;
            }
            throw new Exception("Введите название и цену");
        }

        [HttpGet]
        public void OrderDelete(int orderId)
        {
            var order = APIClient.GetRequest<OrderViewModel>($"api/order/GetOrder?orderId={orderId}");
            APIClient.PostRequest("api/order/DeleteOrder", order);
            Response.Redirect("Order");
        }

        public IActionResult Procedure()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<ProcedureViewModel>>($"api/client/GetClientProcedureList?clientId={Program.Client.Id}"));
        }

        [HttpGet]
        public IActionResult ProcedureCreate()
        {
            return View();
        }

        [HttpPost]
        public void ProcedureCreate(string procedureName, decimal procedurePrice)
        {
            if (!string.IsNullOrEmpty(procedureName))
            {
                APIClient.PostRequest("api/procedure/CreateOrUpdateProcedure", new ProcedureBindingModel
                {
                    ProcedureName = procedureName,
                    ProcedurePrice = procedurePrice,
                    ProcedureOrders = new Dictionary<int, string>(),
                    ClientId = Program.Client.Id
                });
                Response.Redirect("Procedure");
                return;
            }
            throw new Exception("Введите наименование процедуры и стоимость");
        }

        [HttpGet]
        public IActionResult ProcedureUpdate(int procedureId)
        {
            ViewBag.Procedure = APIClient.GetRequest<ProcedureViewModel>($"api/procedure/GetProcedure?procedureId={procedureId}");
            return View();
        }

        [HttpPost]
        public void ProcedureUpdate(int procedureId, string procedureName, decimal procedurePrice)
        {
            if (!string.IsNullOrEmpty(procedureName) && procedurePrice != 0)
            {
                var procedure = APIClient.GetRequest<ProcedureViewModel>($"api/procedure/GetProcedure?procedureId={procedureId}");
                if (procedure == null)
                {
                    return;
                }
                APIClient.PostRequest("api/procedure/CreateOrUpdateProcedure", new ProcedureBindingModel
                {
                    Id = procedure.Id,
                    ProcedureName = procedureName,
                    ProcedurePrice = procedurePrice,
                    ProcedureOrders = procedure.ProcedureOrders,
                    ClientId = Program.Client.Id
                });
                Response.Redirect("Procedure");
                return;
            }
            throw new Exception("Введите наименование процедуры и стоимость");
        }

        [HttpGet]
        public void ProcedureDelete(int procedureId)
        {
            var procedure = APIClient.GetRequest<ProcedureViewModel>($"api/procedure/GetProcedure?procedureId={procedureId}");
            APIClient.PostRequest("api/procedure/DeleteProcedure", procedure);
            Response.Redirect("Procedure");
        }

        public IActionResult Estimate()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<EstimateViewModel>>($"api/client/GetClientEstimateList?clientId={Program.Client.Id}"));
        }

        [HttpGet]
        public IActionResult EstimateCreate()
        {
            ViewBag.Procedures = APIClient.GetRequest<List<ProcedureViewModel>>("api/procedure/GetProcedureList");
            return View();
        }

        [HttpPost]
        public void EstimateCreate(int estimateRate, string comment, int procedureId)
        {
            if (estimateRate>0 && estimateRate <=10 && procedureId!=0)
            {
                APIClient.PostRequest("api/estimate/CreateOrUpdateEstimate", new EstimateBindingModel
                {
                    EstimateRate = estimateRate,
                    Comment = comment,
                    DateEstimate = DateTime.Now,
                    ProcedureId = procedureId,
                    ClientId = Program.Client.Id
                });
                Response.Redirect("Estimate");
                return;
            }
            throw new Exception("Введите оценку(от 1 до 10) и выберите процедуру");
        }

        [HttpGet]
        public IActionResult EstimateUpdate(int estimateId)
        {
            ViewBag.Procedures = APIClient.GetRequest<List<ProcedureViewModel>>("api/procedure/GetProcedureList");
            ViewBag.Estimate = APIClient.GetRequest<EstimateViewModel>($"api/estimate/GetEstimate?estimateId={estimateId}");
            return View();
        }

        [HttpPost]
        public void EstimateUpdate(int estimateId, int estimateRate, string comment, int procedureId)
        {
            if (estimateRate!=0 && estimateRate != 0 && procedureId!=0)
            {
                var estimate = APIClient.GetRequest<EstimateViewModel>($"api/estimate/GetEstimate?estimateId={estimateId}");
                if (estimate == null)
                {
                    return;
                }
                APIClient.PostRequest("api/estimate/CreateOrUpdateEstimate", new EstimateBindingModel
                {
                    Id = estimate.Id,
                    EstimateRate = estimateRate,
                    Comment=comment,
                    DateEstimate = DateTime.Now,
                    ProcedureId = procedureId,
                    ClientId = Program.Client.Id
                });
                Response.Redirect("Estimate");
                return;
            }
            throw new Exception("Введите оценку и выберите процедуру");
        }

        [HttpGet]
        public void EstimateDelete(int estimateId)
        {
            var estimate = APIClient.GetRequest<EstimateViewModel>($"api/estimate/GetEstimate?estimateId={estimateId}");
            APIClient.PostRequest("api/estimate/DeleteEstimate", estimate);
            Response.Redirect("Estimate");
        }

        [HttpGet]
        public IActionResult AddProcedureOrders()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Procedures = APIClient.GetRequest<List<ProcedureViewModel>>("api/procedure/GetProcedureList");
            ViewBag.Orders = APIClient.GetRequest<List<OrderViewModel>>($"api/client/GetClientOrderList?clientId={Program.Client.Id}");
            return View();
        }

        [HttpPost]
        public void AddProcedureOrders(int procedureId, List<int> ordersId)
        {
            if(procedureId!=0 && ordersId != null)
            {
                APIClient.PostRequest("api/procedure/AddProcedureOrders", new AddOrdersBindingModel
                {
                    ProcedureId = procedureId,
                    OrdersId = ordersId
                });
                Response.Redirect("Procedure");
                return;
            }
            throw new Exception("Выберите процедуру и заказы");
        }
    }
}