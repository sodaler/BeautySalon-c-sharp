using BeautySalonContracts.BindingModels;
using BeautySalonContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.StoragesContracts
{
    public interface ILaborCostStorage
    {
        List<LaborCostViewModel> GetFullList();

        List<LaborCostViewModel> GetFilteredList(LaborCostBindingModel model);

        LaborCostViewModel GetElement(LaborCostBindingModel model);

        void Insert(LaborCostBindingModel model);

        void Update(LaborCostBindingModel model);

        void Delete(LaborCostBindingModel model);
    }
}
