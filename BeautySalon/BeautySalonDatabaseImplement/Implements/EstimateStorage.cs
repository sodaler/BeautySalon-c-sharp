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
    public class EstimateStorage : IEstimateStorage
    {
        public List<EstimateViewModel> GetFullList()
        {
            using var context = new BeautySalonDatabase();
            return context.Estimates
                .Include(rec => rec.Procedure)
                .Select(CreateModel)
                .ToList();
        }
        public List<EstimateViewModel> GetFilteredList(EstimateBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new BeautySalonDatabase();
            return context.Estimates
                .Include(rec => rec.Procedure)
                .Where(rec => (rec.Id == model.Id) || (model.ClientId.HasValue && rec.ClientId == model.ClientId)) 
                .Select(CreateModel)
                .ToList();
        }
        public EstimateViewModel GetElement(EstimateBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new BeautySalonDatabase();
            var estimate = context.Estimates
                .Include(rec => rec.Procedure)
                .FirstOrDefault(rec => rec.Id == model.Id);
            return estimate != null ? CreateModel(estimate) : null;
        }
        public void Insert(EstimateBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            context.Estimates.Add(CreateModel(model, new Estimate()));
            context.SaveChanges();
        }
        public void Update(EstimateBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            var element = context.Estimates.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(EstimateBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            Estimate element = context.Estimates.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Estimates.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Estimate CreateModel(EstimateBindingModel model, Estimate estimate)
        {
            estimate.DateEstimate = model.DateEstimate;
            estimate.EstimateRate = model.EstimateRate;
            estimate.Comment = model.Comment;
            estimate.ClientId = (int)model.ClientId;
            estimate.ProcedureId = model.ProcedureId;
            return estimate;
        }
        private static EstimateViewModel CreateModel(Estimate estimate)
        {
            return new EstimateViewModel
            {
                Id = estimate.Id,
                EstimateRate = estimate.EstimateRate,
                Comment = estimate.Comment,
                DateEstimate = estimate.DateEstimate,
                ProcedureId = estimate.ProcedureId,
                ProcedureName = estimate.Procedure.ProcedureName
            };
        }
    }
}
