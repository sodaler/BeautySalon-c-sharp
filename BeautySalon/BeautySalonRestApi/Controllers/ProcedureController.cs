using BeautySalonContracts.BindingModels;
using BeautySalonContracts.BusinessLogicsContracts;
using BeautySalonContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProcedureController : ControllerBase
    {
        private readonly IProcedureLogic _procedureLogic;

        public ProcedureController(IProcedureLogic procedureLogic)
        {
            _procedureLogic = procedureLogic;
        }
               
        [HttpGet]
        public List<ProcedureViewModel> GetProcedureList() => _procedureLogic.Read(null)?.ToList();

        [HttpGet]
        public ProcedureViewModel GetProcedure(int procedureId) => _procedureLogic.Read(new ProcedureBindingModel { Id = procedureId })?[0];

        [HttpPost]
        public void CreateOrUpdateProcedure(ProcedureBindingModel model) => _procedureLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteProcedure(ProcedureBindingModel model) => _procedureLogic.Delete(model);

        [HttpPost]
        public void AddProcedureOrders(AddOrdersBindingModel model) => _procedureLogic.AddOrders(model);
    }
}
