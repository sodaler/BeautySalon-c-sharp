using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautySalonContracts.BindingModels;
using BeautySalonContracts.BusinessLogicsContracts;
using BeautySalonContracts.StoragesContracts;
using BeautySalonContracts.ViewModels;

namespace BeautySalonBusinessLogic.BusinessLogics
{
    public class ProcedureLogic : IProcedureLogic
    {
        private readonly IProcedureStorage _procedureStorage;
        private readonly IOrderStorage _orderStorage;
        public ProcedureLogic(IProcedureStorage procedureStorage, IOrderStorage orderStorage)
        {
            _procedureStorage = procedureStorage;
            _orderStorage = orderStorage;
        }
        public List<ProcedureViewModel> Read(ProcedureBindingModel model)
        {
            if (model == null)
            {
                return _procedureStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ProcedureViewModel> { _procedureStorage.GetElement(model) };
            }
            return _procedureStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(ProcedureBindingModel model)
        {
            var element = _procedureStorage.GetElement(new ProcedureBindingModel
            {
                ProcedureName = model.ProcedureName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть процедура с таким названием");
            }
            if (model.Id.HasValue)
            {
                _procedureStorage.Update(model);
            }
            else
            {
                _procedureStorage.Insert(model);
            }
        }
        public void Delete(ProcedureBindingModel model)
        {
            var element = _procedureStorage.GetElement(new ProcedureBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Удаляемый элемент не найден");
            }
            _procedureStorage.Delete(model);
        }

        public void AddOrders(AddOrdersBindingModel model)
        {
            var procedure = _procedureStorage.GetElement(new ProcedureBindingModel
            {
                Id = model.ProcedureId
            });

            if (procedure == null)
            {
                throw new Exception("Вклад не найден");
            }

            procedure.ProcedureOrders.Clear();

            foreach (var orderId in model.OrdersId)
            {
                var order = _orderStorage.GetElement(new OrderBindingModel
                {
                    Id = orderId
                });

                if (order == null)
                {
                    throw new Exception("Заказ не найден");
                }

                procedure.ProcedureOrders.Add(orderId, order.OrderName);
            }
            _procedureStorage.Update(new ProcedureBindingModel
            {
                Id = procedure.Id,
                ProcedureName = procedure.ProcedureName,
                ProcedurePrice = procedure.ProcedurePrice,
                ClientId = procedure.ClientId,
                ProcedureOrders = procedure.ProcedureOrders
            });
        }
    }
}
