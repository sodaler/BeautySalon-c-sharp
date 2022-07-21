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
    public class LaborCostLogic : ILaborCostLogic
    {
        private readonly ILaborCostStorage _laborCostStorage;
        public LaborCostLogic(ILaborCostStorage laborCostStorage)
        {
            _laborCostStorage = laborCostStorage;
        }
        public void CreateOrUpdate(LaborCostBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _laborCostStorage.Update(model);
            }
            else
            {
                _laborCostStorage.Insert(model);
            }
        }

        public void Delete(LaborCostBindingModel model)
        {
            var element = _laborCostStorage.GetElement(new LaborCostBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _laborCostStorage.Delete(model);
        }

        public List<LaborCostViewModel> Read(LaborCostBindingModel model)
        {
            if (model == null)
            {
                return _laborCostStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<LaborCostViewModel> { _laborCostStorage.GetElement(model) };
            }
            return _laborCostStorage.GetFilteredList(model);
        }
    }
}
