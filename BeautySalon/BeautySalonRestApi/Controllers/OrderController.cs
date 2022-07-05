using BeautySalonContracts.BindingModels;
using BeautySalonContracts.BusinessLogicsContracts;
using BeautySalonContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderLogic _orderLogic;
        private readonly ICosmeticLogic _cosmeticLogic;
        
        public OrderController(IOrderLogic orderLogic, ICosmeticLogic cosmeticLogic)
        {
            _orderLogic = orderLogic;
            _cosmeticLogic = cosmeticLogic;
        }

        [HttpGet]
        public List<OrderViewModel> GetOrderList() => _orderLogic.Read(null)?.ToList();

        [HttpGet]
        public List<CosmeticViewModel> GetCosmeticList() => _cosmeticLogic.Read(null)?.ToList();

        [HttpGet]
        public CosmeticViewModel GetCosmetic(int cosmeticId) => _cosmeticLogic.Read(new CosmeticBindingModel { Id = cosmeticId })?[0];

        [HttpGet]
        public OrderViewModel GetOrder(int orderId) => _orderLogic.Read(new OrderBindingModel { Id = orderId })?[0];

        [HttpPost]
        public void CreateOrUpdateOrder(OrderBindingModel model) => _orderLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteOrder(OrderBindingModel model) => _orderLogic.Delete(model);        
    }
}
