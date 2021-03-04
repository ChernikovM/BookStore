﻿using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.BusinessLogicLayer.Providers.Interfaces
{
    public interface IDataCollectionAccessProvider
    {
        public List<T> GetCollection<T, U>(IQueryable<U> collection, IndexRequestModel requestModel, out DataCollectionModel<T> responseModel);
        
        public IQueryable<T> Pagination<T>(IQueryable<T> collection, ref PageRequestModel requestModel, out PageModel responseModel);

        public IQueryable<T> Filter<T, U>(IQueryable<T> collection, ref string filterString);

        public IQueryable<T> Sort<T, U>(IQueryable<T> collection, ref string sortString);
    }
}