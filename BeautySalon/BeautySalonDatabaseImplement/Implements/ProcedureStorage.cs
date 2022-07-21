using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautySalonContracts.StoragesContracts;
using BeautySalonContracts.ViewModels;
using BeautySalonContracts.BindingModels;
using BeautySalonDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonDatabaseImplement.Implements
{
    public class ProcedureStorage : IProcedureStorage
    {
        public List<ProcedureViewModel> GetFullList()
        {
            using var context = new BeautySalonDatabase();
            return context.Procedures
            .Include(rec => rec.ProcedureOrders)
            .ThenInclude(rec => rec.Order)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<ProcedureViewModel> GetFilteredList(ProcedureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new BeautySalonDatabase();
            return context.Procedures
            .Include(rec => rec.ProcedureOrders)
            .ThenInclude(rec => rec.Order)
            .Where(rec => (rec.ProcedureName.Contains(model.ProcedureName)) || (model.ClientId.HasValue && rec.ClientId == model.ClientId))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public ProcedureViewModel GetElement(ProcedureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new BeautySalonDatabase();
            var procedure = context.Procedures
            .Include(rec => rec.ProcedureOrders)
            .ThenInclude(rec => rec.Order)
            .FirstOrDefault(rec => rec.ProcedureName == model.ProcedureName || rec.Id == model.Id);
            return procedure != null ? CreateModel(procedure) : null;
        }
        public void Insert(ProcedureBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Procedure procedure = new Procedure()
                {
                    ProcedureName = model.ProcedureName,
                    ProcedurePrice = model.ProcedurePrice,
                    ClientId = (int)model.ClientId
                };
                context.Procedures.Add(procedure);
                context.SaveChanges();
                CreateModel(model, procedure, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(ProcedureBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Procedures.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(ProcedureBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            Procedure element = context.Procedures.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                var procedureOrders = context.OrderProcedures.Where(rec => rec.ProcedureId == element.Id).ToList();
                context.OrderProcedures.RemoveRange(procedureOrders);
                context.Procedures.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Procedure CreateModel(ProcedureBindingModel model, Procedure procedure, BeautySalonDatabase context)
        {
            procedure.ProcedureName = model.ProcedureName;
            procedure.ProcedurePrice = model.ProcedurePrice;
            procedure.ClientId = (int)model.ClientId;
            if (model.Id.HasValue)
            {
                var orderProcedures = context.OrderProcedures.Where(rec => rec.ProcedureId == model.Id.Value).ToList();
                context.OrderProcedures.RemoveRange(orderProcedures);
                context.SaveChanges();
            }
            // добавили новые
            foreach (var cd in model.ProcedureOrders)
            {
                context.OrderProcedures.Add(new OrderProcedure
                {
                    ProcedureId = procedure.Id,
                    OrderId = cd.Key,
                });
                context.SaveChanges();
            }            
            return procedure;
        }
        private static ProcedureViewModel CreateModel(Procedure procedure)
        {
            return new ProcedureViewModel
            {
                Id = procedure.Id,
                ClientId = procedure.ClientId,
                ProcedureName = procedure.ProcedureName,
                ProcedurePrice = procedure.ProcedurePrice,
                ProcedureOrders = procedure.ProcedureOrders
                .ToDictionary(recDC => recDC.OrderId, recDC => recDC.Order?.OrderName)
            };
        }
    }
}
