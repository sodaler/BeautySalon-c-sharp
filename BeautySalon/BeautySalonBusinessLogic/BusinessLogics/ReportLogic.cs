using BeautySalonBusinessLogic.OfficePackage;
using BeautySalonBusinessLogic.OfficePackage.HelperModels;
using BeautySalonContracts.BindingModels;
using BeautySalonContracts.BusinessLogicsContracts;
using BeautySalonContracts.StoragesContracts;
using BeautySalonContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IProcedureStorage _procedureStorage;
        private readonly ICosmeticStorage _cosmeticStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly IServiceStorage _serviceStorage;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToPdf _saveToPdf;


        public ReportLogic(IProcedureStorage procedureStorage, ICosmeticStorage cosmeticStorage,
            IOrderStorage orderStorage, IServiceStorage serviceStorage, AbstractSaveToWord saveToWord,
            AbstractSaveToExcel saveToExcel, AbstractSaveToPdf saveToPdf)
        {
            _procedureStorage = procedureStorage;
            _cosmeticStorage = cosmeticStorage;
            _orderStorage = orderStorage;
            _serviceStorage = serviceStorage;
            _saveToWord = saveToWord;
            _saveToExcel = saveToExcel;
            _saveToPdf = saveToPdf;
        }

        public List<ReportOrderServiceViewModel> GetOrderService(ReportBindingModel model)
        {
            var orders = model.Orders;
            var list = new List<ReportOrderServiceViewModel>();
            foreach (var order in orders)
            {
                var record = new ReportOrderServiceViewModel
                {
                    OrderName = order.OrderName,
                    Services = new List<string>()
                };
                foreach (var cosmeticKVP in order.OrderCosmetics)
                {
                    var lp = _cosmeticStorage.GetElement(new CosmeticBindingModel { Id = cosmeticKVP.Key });
                    foreach (var service in lp.CosmeticServices)
                    {
                        record.Services.Add(service.Value.Item1);
                    }
                }
                var procedures = _procedureStorage.GetFullList().Where(rec => rec.ProcedureOrders.Keys.ToList().Contains(order.Id)).ToList();
                foreach (var procedure in procedures)
                {
                    var services = _serviceStorage.GetFullList().Where(rec => rec.ServiceProcedures.Keys.ToList().Contains(procedure.Id)).ToList();
                    record.Services.AddRange(services.Select(cur => cur.ServiceName));
                }
                record.Services = record.Services.Distinct().ToList();
                list.Add(record);
            }
            return list;
        }

        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            var list = new List<ReportOrdersViewModel>();
            var orders = _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                ClientId = model.ClientId
            });
            foreach (var order in orders)
            {
                var record = new ReportOrdersViewModel
                {
                    OrderName = order.OrderName,
                    CreateDate = order.CreateDate,
                    ProcedureServices = new List<(ProcedureViewModel, List<ServiceViewModel>)>()
                };
                var procedures = _procedureStorage.GetFullList().Where(rec => rec.ProcedureOrders.Keys.ToList().Contains(order.Id)).ToList();
                foreach (var procedure in procedures)
                {
                    var services = _serviceStorage.GetFullList().Where(rec => rec.ServiceProcedures.Keys.ToList().Contains(procedure.Id)).ToList();
                    record.ProcedureServices.Add((procedure, services));
                }
                list.Add(record);
            }
            return list;
        }

        public List<ReportServicesViewModel> GetServices(ReportBindingModel model)
        {

            var list = new List<ReportServicesViewModel>();
            var services = _serviceStorage.GetFilteredList(new ServiceBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                EmployeeId = model.EmployeeId
            });
            foreach (var service in services)
            {
                var ProcedureList = new List<ProcedureViewModel>();
                var record = new ReportServicesViewModel
                {
                    ServiceName = service.ServiceName,
                    DateAdding = service.DateAdding,
                    Procedures = string.Empty,
                    Cosmetics = string.Empty,
                };
                foreach (var procedureKVP in service.ServiceProcedures)
                {
                    var procedure = _procedureStorage.GetElement(new ProcedureBindingModel { Id = procedureKVP.Key });
                    ProcedureList.Add(procedure);
                }
                record.Procedures = string.Join(", ", ProcedureList.Select(dep=>dep.ProcedureName));
                record.Cosmetics = string.Join(", ",_cosmeticStorage.GetFullList()
                    .Where(rec => rec.CosmeticServices.Keys.ToList().Contains(service.Id)).ToList().Select(lp => lp.CosmeticName).ToList());
                list.Add(record);
            }

            return list;
        }

        public List<ReportCosmeticProcedureViewModel> GetCosmeticProcedure(ReportBindingModel model)
        {
            var cosmetics = model.Cosmetics;
            var list = new List<ReportCosmeticProcedureViewModel>();
            foreach (var cosmetic in cosmetics)
            {
                var record = new ReportCosmeticProcedureViewModel
                {
                    CosmeticName = cosmetic.CosmeticName,
                    Procedures = new List<Tuple<string, decimal>>(),
                };
                foreach (var serviceKVP in cosmetic.CosmeticServices)
                {
                    var service = _serviceStorage.GetElement(new ServiceBindingModel { Id = serviceKVP.Key });
                    foreach (var procedure in service.ServiceProcedures)
                    {
                        record.Procedures.Add(new Tuple<string, decimal>(procedure.Value.Item1, procedure.Value.Item2));
                    }
                }
                list.Add(record);
            }
            return list;
        }

        public void SaveOrderServiceToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReportClient(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Услуги по заказам",
                OrderService = GetOrderService(model)
            });
        }

        public void SaveOrderServiceToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDocClient(new WordInfo
            {
                FileName = model.FileName,
                Title = "Услуги по заказам",
                OrderService = GetOrderService(model),
            });
        }

        public void SaveCosmeticProcedureToExcelFile(ReportBindingModel model)
        {
           
            _saveToExcel.CreateReportManager(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Процедуры по косметикам",
                CosmeticProcedure = GetCosmeticProcedure(model)
            });
        }

        public void SaveCosmeticProcedureToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDocManager(new WordInfo
            {
                FileName = model.FileName,
                Title = "Процедуры по косметикам",
                CosmeticProcedure = GetCosmeticProcedure(model),
            });
        }
        public void SaveServicesToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDocManager(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Сведения по услуге",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Services = GetServices(model)
            });
        }
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDocClient(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Сведения по заказам",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }
}
