using BeautySalonContracts.BindingModels;
using BeautySalonContracts.BusinessLogicsContracts;
using BeautySalonContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EstimateController : ControllerBase
    {
        private readonly IEstimateLogic _estimateLogic;
        public EstimateController(IEstimateLogic estimateLogic)
        {
            _estimateLogic = estimateLogic;
        }

        [HttpGet]
        public List<EstimateViewModel> GetEstimateList() => _estimateLogic.Read(null)?.ToList();

        [HttpGet]
        public EstimateViewModel GetEstimate(int estimateId) => _estimateLogic.Read(new EstimateBindingModel { Id = estimateId })?[0];

        [HttpPost]
        public void CreateOrUpdateEstimate(EstimateBindingModel model) => _estimateLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteEstimate(EstimateBindingModel model) => _estimateLogic.Delete(model);
    }
}
