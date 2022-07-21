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
    public class CosmeticStorage : ICosmeticStorage
    {
        public void Delete(CosmeticBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            Cosmetic element = context.Cosmetics.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element != null)
            {
                context.CosmeticServices.RemoveRange(context.CosmeticServices.Where(rec => rec.CosmeticId == element.Id));
                context.SaveChanges();
                context.Cosmetics.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }

        }

        public CosmeticViewModel GetElement(CosmeticBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new BeautySalonDatabase();
            var cosmetic = context.Cosmetics
            .Include(rec => rec.OrderCosmetics)
            .ThenInclude(rec => rec.Order)
            .Include(rec => rec.CosmeticServices)
            .ThenInclude(rec => rec.Service)
            .FirstOrDefault(rec => rec.CosmeticName == model.CosmeticName ||
            rec.Id == model.Id);
            return cosmetic != null ? CreateModel(cosmetic) : null;
        }

        public List<CosmeticViewModel> GetFilteredList(CosmeticBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new BeautySalonDatabase();
            return context.Cosmetics
            .Include(rec => rec.OrderCosmetics)
            .ThenInclude(rec => rec.Order)
            .Include(rec => rec.CosmeticServices)
            .ThenInclude(rec => rec.Service)
            .Where(rec => rec.CosmeticName.Contains(model.CosmeticName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<CosmeticViewModel> GetFullList()
        {
            using var context = new BeautySalonDatabase();
            return context.Cosmetics
            .Include(rec => rec.OrderCosmetics)
            .ThenInclude(rec => rec.Order)
            .Include(rec => rec.CosmeticServices)
            .ThenInclude(rec => rec.Service)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(CosmeticBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Cosmetic cosmetic = new Cosmetic()
                {
                    CosmeticName = model.CosmeticName,
                    Price = model.Price,
                    ManagerId = (int)model.EmployeeId
                };
                context.Cosmetics.Add(cosmetic);
                context.SaveChanges();
                CreateModel(model, cosmetic, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(CosmeticBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Cosmetics.FirstOrDefault(rec => rec.Id ==
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
        private static Cosmetic CreateModel(CosmeticBindingModel model, Cosmetic cosmetic, BeautySalonDatabase context)
        {
            cosmetic.CosmeticName = model.CosmeticName;
            cosmetic.Price = model.Price;
            if (model.Id.HasValue)
            {
                var lpCurreuncies = context.CosmeticServices.Where(rec =>
                rec.CosmeticId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.CosmeticServices.RemoveRange(lpCurreuncies.Where(rec =>
                !model.CosmeticServices.ContainsKey(rec.ServiceId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateIngredient in lpCurreuncies)
                {
                    model.CosmeticServices.Remove(updateIngredient.ServiceId);
                }
                context.SaveChanges();
            }
            foreach (var wi in model.CosmeticServices)
            {
                context.CosmeticServices.Add(new CosmeticService
                {
                    CosmeticId = cosmetic.Id,
                    ServiceId = wi.Key,
                });
                context.SaveChanges();
            }
            return cosmetic;
        }
    
        private static CosmeticViewModel CreateModel(Cosmetic cosmetic)
        {
            return new CosmeticViewModel
            {
                Id = cosmetic.Id,
                CosmeticName = cosmetic.CosmeticName,
                Price = cosmetic.Price,
                CosmeticServices = cosmetic.CosmeticServices
            .ToDictionary(recII => recII.ServiceId,
            recII => (recII.Service?.ServiceName, recII.Service.ServicePrice))
            };
        }
    }
}
