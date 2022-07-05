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
    public class LaborCostStorage : ILaborCostStorage
    {
        public void Delete(LaborCostBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            LaborCost element = context.LaborCosts.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.LaborCosts.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public LaborCostViewModel GetElement(LaborCostBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new BeautySalonDatabase();
            var order = context.LaborCosts
            .Include(rec => rec.Cosmetic)
            .Include(rec => rec.Manager)
            .FirstOrDefault(rec => rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }

        public List<LaborCostViewModel> GetFilteredList(LaborCostBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new BeautySalonDatabase();
            return context.LaborCosts
            .Include(rec => rec.Cosmetic)
            .Include(rec => rec.Manager)
            .Where(rec => rec.Id == model.Id)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<LaborCostViewModel> GetFullList()
        {
            using var context = new BeautySalonDatabase();
            return context.LaborCosts
            .Include(rec => rec.Cosmetic)
            .Include(rec => rec.Manager)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(LaborCostBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.LaborCosts.Add(CreateModel(model, new LaborCost()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(LaborCostBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.LaborCosts.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        private static LaborCost CreateModel(LaborCostBindingModel model, LaborCost laborCost)
        {
            laborCost.CosmeticId = (int)model.CosmeticId;
            laborCost.ManagerId = (int)model.EmployeeId;
            laborCost.StartLaborCost = model.StartLaborCost;
            laborCost.EndLaborCost = model.EndLaborCost;
            return laborCost;
        }
        private static LaborCostViewModel CreateModel(LaborCost laborCost)
        {
            return new LaborCostViewModel
            {
                Id = laborCost.Id,
                CosmeticId = laborCost.CosmeticId,
                CosmeticName = laborCost.Cosmetic.CosmeticName,
                StartLaborCost = laborCost.StartLaborCost,
                EndLaborCost = laborCost.EndLaborCost
            };
        }
    }
}
