using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface ICrudService<TModel, TCreateModel>
        where TModel : BaseErrorModel
    {
        Task<TModel> GetAsync(long id);

        Task<DataCollectionModel<TModel>> GetAllAsync(IndexRequestModel model);

        Task CreateAsync(TCreateModel model);

        Task RemoveAsync(long id);

        Task UpdateAsync(long id, TModel model);
    }
}
