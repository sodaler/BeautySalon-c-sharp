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
    public class EmployeeStorage : IEmployeeStorage
    {
        public void Delete(EmployeeBindingModel model)
        {
            using var context = new BeautySalonDatabase();
            Employee element = context.Emplyees.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Emplyees.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Заказ не найден");
            }
        }

        public EmployeeViewModel GetElement(EmployeeBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new BeautySalonDatabase();

            var manager = context.Emplyees.Include(x => x.LaborCosts).Include(x => x.Cosmetics).Include(x => x.Services)
            .FirstOrDefault(rec => rec.Email == model.Email ||
            rec.Id == model.Id);
            return manager != null ?
            new EmployeeViewModel
            {
                Id = manager.Id,
                ManagerFIO = manager.ManagerFIO,
                Email = manager.Email,
                Password = manager.Password,
            } :
            null;
        }

        public List<EmployeeViewModel> GetFilteredList(EmployeeBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new BeautySalonDatabase();

            return context.Emplyees.Include(x => x.LaborCosts).Include(x => x.Cosmetics).Include(x => x.Services)
            .Where(rec => rec.Email == model.Email && rec.Password == model.Password)
            .Select(rec => new EmployeeViewModel
            {
                Id = rec.Id,
                ManagerFIO = rec.ManagerFIO,
                Email = rec.Email,
                Password = rec.Password,
            })
            .ToList();
        }

        public List<EmployeeViewModel> GetFullList()
        {
            using var context = new BeautySalonDatabase();

            return context.Emplyees.Select(rec => new EmployeeViewModel
            {
                Id = rec.Id,
                ManagerFIO = rec.ManagerFIO,
                Email = rec.Email,
                Password = rec.Password,
            })
            .ToList();
        }

        public void Insert(EmployeeBindingModel model)
        {
            using var context = new BeautySalonDatabase();

            context.Emplyees.Add(CreateModel(model, new Employee()));
            context.SaveChanges();
        }

        public void Update(EmployeeBindingModel model)
        {
            using var context = new BeautySalonDatabase();

            var element = context.Emplyees.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Руководитель не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        private Employee CreateModel(EmployeeBindingModel model, Employee manager)
        {
            manager.ManagerFIO = model.EmployeeFIO;
            manager.Email = model.Email;
            manager.Password = model.Password;
            return manager;
        }
    }
}
