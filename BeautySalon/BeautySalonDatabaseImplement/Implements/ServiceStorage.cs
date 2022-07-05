using BeautySalonContracts.BindingModels;
using BeautySalonContracts.StoragesContracts;
using BeautySalonContracts.ViewModels;
using BeautySalonDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonDatabaseImplement.Implements
{
    public class ServiceStorage : IServiceStorage
    {
        public void Delete(ServiceBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            Service element = context.Services.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Services.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public ServiceViewModel GetElement(ServiceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new BeautySalonDatabase();
            var service = context.Services
            .Include(rec => rec.CosmeticServices)
            .ThenInclude(rec => rec.Cosmetic)
            .Include(rec => rec.ProcedureServices)
            .ThenInclude(rec => rec.Procedure)
            .Include(rec => rec.Manager)
            .FirstOrDefault(rec => rec.ServiceName == model.ServiceName || rec.Id == model.Id);
            return service != null ? CreateModel(service) : null;
        }

        public List<ServiceViewModel> GetFilteredList(ServiceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new BeautySalonDatabase();

            return context.Services
            .Include(rec => rec.CosmeticServices)
            .ThenInclude(rec => rec.Cosmetic)
            .Include(rec => rec.ProcedureServices)
            .ThenInclude(rec => rec.Procedure)
            .Include(rec => rec.Manager)
            .Where(rec => (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateAdding.Date >= model.DateFrom.Value.Date && rec.DateAdding.Date <= model.DateTo.Value.Date && 
            model.EmployeeId.HasValue && rec.ManagerId == model.EmployeeId))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<ServiceViewModel> GetFullList()
        {
            using var context = new BeautySalonDatabase();
            return context.Services
            .Include(rec => rec.CosmeticServices)
            .ThenInclude(rec => rec.Cosmetic)
            .Include(rec => rec.ProcedureServices)
            .ThenInclude(rec => rec.Procedure)
            .Include(rec => rec.Manager)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(ServiceBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Services.Add(CreateModel(model, new Service(), context));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(ServiceBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Services.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        private static Service CreateModel(ServiceBindingModel model, Service service, BeautySalonDatabase context)
        {
            service.ServiceName = model.ServiceName;
            service.ServicePrice = model.ServicePrice;
            service.ManagerId = (int)model.EmployeeId.GetValueOrDefault(service.ManagerId);
            service.DateAdding = model.DateAdding;
            if (model.Id.HasValue)
            {
                var serviceProcedure = context.ProcedureServices.Where(rec => rec.ServiceId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.ProcedureServices.RemoveRange(serviceProcedure.Where(rec => !model.ServiceProcedures.ContainsKey(rec.ProcedureId)));
                context.SaveChanges();
            }
            // добавили новые
            if (model.ServiceProcedures != null)
            {
                foreach (var cd in model.ServiceProcedures)
                {
                    var depCur = context.ProcedureServices.SingleOrDefault(rec => rec.ProcedureId == cd.Key && rec.ServiceId == service.Id);
                    if (depCur == null)
                    {
                        context.ProcedureServices.Add(new ProcedureService
                        {
                            ProcedureId = cd.Key,
                            ServiceId = service.Id,
                        });
                        context.SaveChanges();
                    }
                }
            }
            
            return service;
        }
        private static ServiceViewModel CreateModel(Service service)
        {
            return new ServiceViewModel
            {
                Id = service.Id,
                ServiceName = service.ServiceName,
                ServicePrice = service.ServicePrice,
                DateAdding = service.DateAdding,
                ServiceProcedures= service.ProcedureServices
            .ToDictionary(recII => recII.ProcedureId,
            recII => (recII.Procedure?.ProcedureName, recII.Procedure.ProcedurePrice))
            };
        }
    }
}
