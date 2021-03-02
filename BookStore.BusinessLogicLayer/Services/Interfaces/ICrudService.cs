using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface ICrudService<TModel>
        where TModel : BaseErrorModel
    {
        Task<TModel> GetAsync(TModel model);

        Task<DataCollectionModel<TModel>> GetAllAsync(IndexRequestModel model);

        Task CreateAsync(TModel model);

        Task RemoveAsync(TModel model);

        Task UpdateAsync(TModel model);
    }
}
