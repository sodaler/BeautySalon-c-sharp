using BeautySalonContracts.BindingModels;
using BeautySalonContracts.BusinessLogicsContracts;
using BeautySalonContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientLogic _clerkLogic;
        private readonly IProcedureLogic _procedureLogic;
        private readonly IEstimateLogic _estimateLogic;
        private readonly IOrderLogic _orderLogic;
        public ClientController(IClientLogic logic, IProcedureLogic depositLogic, IEstimateLogic replenishmentLogic, IOrderLogic orderLogic)
        {
            _clerkLogic = logic;
            _procedureLogic = depositLogic;
            _estimateLogic = replenishmentLogic;
            _orderLogic = orderLogic;
        }

        [HttpGet]
        public ClientViewModel Login(string login, string password)
        {
            var list = _clerkLogic.Read(new ClientBindingModel
            {
                Email = login,
                Password = password
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        [HttpPost]
        public void Register(ClientBindingModel model) => _clerkLogic.CreateOrUpdate(model);

        [HttpPost]
        public void UpdateData(ClientBindingModel model) => _clerkLogic.CreateOrUpdate(model);

        [HttpGet]
        public List<ProcedureViewModel> GetClientProcedureList(int clientId) => _procedureLogic.Read(new ProcedureBindingModel { ClientId = clientId });

        [HttpGet]
        public List<OrderViewModel> GetClientOrderList(int clientId) => _orderLogic.Read(new OrderBindingModel { ClientId = clientId });

        [HttpGet]
        public List<EstimateViewModel> GetClientEstimateList(int clientId) => _estimateLogic.Read(new EstimateBindingModel { ClientId = clientId });
    }
}

