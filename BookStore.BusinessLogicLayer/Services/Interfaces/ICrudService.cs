using BookStore.BusinessLogicLayer.Models;
using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.Responses;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface ICrudService<TModel>
        where TModel : BaseModel
    {
        TModel Get(TModel model);

        DataCollectionModel<TModel> GetAll(IndexRequestModel model);

        void Create(TModel model);

        void Remove(TModel model);

        void Update(TModel model);
    }
}
