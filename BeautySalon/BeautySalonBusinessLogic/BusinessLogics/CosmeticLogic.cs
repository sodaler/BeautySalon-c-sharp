using BeautySalonContracts.BindingModels;
using BeautySalonContracts.BusinessLogicsContracts;
using BeautySalonContracts.StoragesContracts;
using BeautySalonContracts.ViewModels;


namespace BeautySalonBusinessLogic.BusinessLogics
{
    public class CosmeticLogic : ICosmeticLogic
    {
        private readonly ICosmeticStorage _cosmeticStorage;
        public CosmeticLogic (ICosmeticStorage cosmeticStorage)
        {
            _cosmeticStorage = cosmeticStorage;
        }
        public void CreateOrUpdate(CosmeticBindingModel model)
        {
            var cosmetic = _cosmeticStorage.GetElement(new CosmeticBindingModel { CosmeticName = model.CosmeticName });
            if (cosmetic != null && cosmetic.Id != model.Id)
            {
                throw new Exception("Такая косметика уже есть");
            }
            if (model.Id.HasValue)
            {
                _cosmeticStorage.Update(model);
            }
            else
            {
                _cosmeticStorage.Insert(model);
            }
        }

        public void Delete(CosmeticBindingModel model)
        {
            var element = _cosmeticStorage.GetElement(new CosmeticBindingModel{ Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _cosmeticStorage.Delete(model);
        }

        public List<CosmeticViewModel> Read(CosmeticBindingModel model)
        {
            if (model == null)
            {
                return _cosmeticStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<CosmeticViewModel> { _cosmeticStorage.GetElement(model) };
            }
            return _cosmeticStorage.GetFilteredList(model);
        }
    }
}
