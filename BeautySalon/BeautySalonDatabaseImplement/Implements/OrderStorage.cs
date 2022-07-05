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
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using var context = new BeautySalonDatabase();
            return context.Orders
            .Include(rec => rec.OrderProcedures)
            .ThenInclude(rec => rec.Procedure)
            .Include(rec => rec.OrderCosmetics)
            .ThenInclude(rec => rec.Cosmetic)
            .Select(CreateModel)
            .ToList();
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new BeautySalonDatabase();
            return context.Orders
            .Include(rec => rec.OrderProcedures)
            .ThenInclude(rec => rec.Procedure)
            .Include(rec => rec.OrderCosmetics)
            .ThenInclude(rec => rec.Cosmetic)
            .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.CreateDate.Date == model.DateVisit.Date) ||
            (model.DateFrom.HasValue && model.DateTo.HasValue && rec.CreateDate.Date >= model.DateFrom.Value.Date && rec.CreateDate.Date <= model.DateTo.Value.Date) ||
            (model.ClientId.HasValue && rec.ClientId == model.ClientId))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new BeautySalonDatabase();
            var order = context.Orders
            .Include(rec => rec.OrderProcedures)
            .ThenInclude(rec => rec.Procedure)
            .Include(rec => rec.OrderCosmetics)
            .ThenInclude(rec => rec.Cosmetic)
            .FirstOrDefault(rec => rec.Price == model.Price || rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }
        public void Insert(OrderBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Order order = new Order()
                {
                    OrderName = model.OrderName,
                    Price = model.Price,
                    CreateDate = model.DateVisit,
                    ClientId = (int)model.ClientId
                };
                context.Orders.Add(order);
                context.SaveChanges();
                CreateModel(model, order, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(OrderBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(OrderBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Orders.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Order CreateModel(OrderBindingModel model, Order order, BeautySalonDatabase context)
        {
            order.OrderName = model.OrderName;
            order.ClientId = (int)model.ClientId;
            order.Price = model.Price;
            order.CreateDate = model.DateVisit;
            if (model.Id.HasValue)
            {
                // получам текущие записи
                var orderCosmetics = context.OrderCosmetics.Where(rec => rec.OrderId == model.Id.Value).ToList();
                context.OrderCosmetics.RemoveRange(orderCosmetics);
                context.SaveChanges();
            }
            // добавили новые
            foreach (var clp in model.OrderCosmetics)
            {
                context.OrderCosmetics.Add(new OrderCosmetic
                {
                    OrderId = order.Id,
                    CosmeticId = clp.Key
                });
                context.SaveChanges();
            }
            return order;
        }
        private static OrderViewModel CreateModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                OrderName = order.OrderName,
                Price = order.Price,
                CreateDate = order.CreateDate,
                OrderCosmetics = order.OrderCosmetics
                .ToDictionary(recCLP => recCLP.CosmeticId, recCLP => recCLP.Cosmetic?.CosmeticName)
            };
        }
    }
}
