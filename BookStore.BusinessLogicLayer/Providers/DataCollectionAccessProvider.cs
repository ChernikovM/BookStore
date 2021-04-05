﻿using AutoMapper;
using BookStore.BusinessLogicLayer.Configurations.Interfaces;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Web;

namespace BookStore.BusinessLogicLayer.Services
{
    public class DataCollectionAccessProvider : IDataCollectionAccessProvider
    {
        private readonly IDataCollectionAccessProviderConfiguration _config;
        private readonly IMapper _mapper;

        public DataCollectionAccessProvider(IDataCollectionAccessProviderConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }

        public List<TDest> GetCollection<TSource, TDest>(IQueryable<TSource> collection, IndexRequestModel requestModel, out DataCollectionModel<TDest> responseModel)
        {
            string filter = requestModel.Filter;
            string sort = requestModel.SortBy;
            var pagination = requestModel.PageRequestModel;

            collection = Filter<TSource, TDest>(collection, ref filter);
            collection = Sort<TSource, TDest>(collection, ref sort);
            collection = Pagination<TSource>(collection, ref pagination, out PageModel pageModel, out int collectionCount);

            var result = _mapper.Map<List<TSource>, List<TDest>>(collection.ToList());

            responseModel = new DataCollectionModel<TDest>()
            {
                Sort = sort,
                Filter = filter,
                PageModel = pageModel,
                DataCollection = result,
                CollectionCount = collectionCount
            };

            return result;
        }
        
        public IQueryable<T> Filter<T, U>(IQueryable<T> collection, ref string filterString)
        {
            if (filterString is null)
            {
                return collection;
            }

            string[] filters = filterString.Split(_config.SplitCharacter);

            filterString = string.Empty;

            foreach (var filter in filters)
            {
                try
                {
                    string propName = filter.Split(new char[] {'.', '=', '>', '<'}).First();
                    string expr = filter;
                    var propType = GetPropertyType(typeof(U), propName);
                    if (propType is null)
                    {
                        continue;
                    }
                    collection = collection.Where(filter);

                    filterString += filter + _config.SplitCharacter; //if exception was not invoked
                }
                catch (Exception)
                {
                    //do nothing if property was not found or other
                }
            }

            return collection;
        }

        public IQueryable<T> Pagination<T>(IQueryable<T> collection, ref PageRequestModel requestModel, out PageModel responseModel, out int collectionCount)
        {
            collectionCount = collection.Count();

            if (requestModel.Page is null || requestModel.Page < 1)
            {
                requestModel.Page = _config.DefaultPage;
            }

            responseModel = new PageModel(collectionCount, requestModel.Page.Value, requestModel.PageSize.Value);

            if (requestModel.Page.Value > responseModel.TotalPages)
            {
                requestModel.Page = responseModel.TotalPages;
                responseModel.CurrentPageNumber = requestModel.Page.Value;
            }

            if (collectionCount == 0)
            {
                return collection;
            }

            collection = collection.Page(requestModel.Page.Value, requestModel.PageSize.Value);

            return collection;
        }

        public IQueryable<T> Sort<T, U>(IQueryable<T> collection, ref string sortString)
        {
            if (sortString is null)
            {
                return collection;
            }

            string propName = sortString.Split(_config.SplitCharacter).First();
            var propType = GetPropertyType(typeof(U), propName);

            if (propType is null)
            {
                sortString = string.Empty;
                return collection;
            }

            try
            {
                collection = collection.OrderBy($"{HttpUtility.UrlDecode(sortString)}");
            }
            catch (Exception)
            {
                sortString = string.Empty;
            }
            return collection;
        }

        private Type GetPropertyType(Type type, string propertyName)
        {
            var props = type.GetProperties().ToList();
            var prop = props.FirstOrDefault(x => x.Name.Equals(propertyName));

            if (prop is null)
            {
                return null;
            }
            return prop.PropertyType;
        }
    }
}
