using BookStore.BusinessLogicLayer.Models;
using BookStore.BusinessLogicLayer.Models.Responses;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IDataCollectionAccessService
    {
        public List<T> GetCollection<T, U>(IQueryable<U> collection, IndexRequestModel requestModel, out DataCollectionModel<T> responseModel);
        
        public IQueryable<T> Pagination<T>(IQueryable<T> collection, ref PageRequestModel requestModel, out PageModel responseModel);

        public IQueryable<T> Filter<T>(IQueryable<T> collection, ref string filterString);

        public IQueryable<T> Sort<T>(IQueryable<T> collection, ref string sortString);
    }
}
