using BeautySalonContracts.BindingModels;
using BeautySalonContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonContracts.BusinessLogicsContracts
{
    public interface ILaborCostLogic
    {
        List<LaborCostViewModel> Read(LaborCostBindingModel model);
        void CreateOrUpdate(LaborCostBindingModel model);
        void Delete(LaborCostBindingModel model);
    }
}
