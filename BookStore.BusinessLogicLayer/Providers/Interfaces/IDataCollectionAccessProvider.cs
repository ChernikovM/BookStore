using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.BusinessLogicLayer.Providers.Interfaces
{
    public interface IDataCollectionAccessProvider
    {
        public List<U> GetCollection<T, U>(IQueryable<T> collection, IndexRequestModel requestModel, out DataCollectionModel<U> responseModel);
        
        public IQueryable<T> Pagination<T>(IQueryable<T> collection, ref PageRequestModel requestModel, out PageModel responseModel, out int collectionCount);

        public IQueryable<T> Filter<T, U>(IQueryable<T> collection, ref string filterString);

        public IQueryable<T> Sort<T, U>(IQueryable<T> collection, ref string sortString);
    }
}
