﻿using AutoMapper;
using BookStore.BusinessLogicLayer.Configurations.Interfaces;
using BookStore.BusinessLogicLayer.Models;
using BookStore.BusinessLogicLayer.Models.Responses;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Web;

namespace BookStore.BusinessLogicLayer.Services
{
    public class DataCollectionAccessService : IDataCollectionAccessService
    {
        private readonly IDataCollectionAccessServiceConfiguration _config;
        private readonly IMapper _mapper;

        public DataCollectionAccessService(IDataCollectionAccessServiceConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }

        public List<T> GetCollection<T, U>(IQueryable<U> collection, IndexRequestModel requestModel, out DataCollectionModel<T> responseModel)
        {
            string filter = requestModel.Filter;
            string sort = requestModel.SortBy;
            var pagination = requestModel.PageRequestModel;

            collection = Filter<U, T>(collection, ref filter);
            collection = Sort<U, T>(collection, ref sort);

            collection = Pagination<U>(collection, ref pagination, out PageModel pageModel);

            var result = _mapper.Map<List<U>, List<T>>(collection.ToList());

            responseModel = new DataCollectionModel<T>()
            {
                Sort = sort,
                Filter = filter,
                PageModel = pageModel,
                DataCollection = result
            };

            return result;
        }
        
        public IQueryable<T> Filter<T, U>(IQueryable<T> collection, ref string filterString)
        {
            if (filterString is null)
            {
                return collection;
            }

            string[] filters = filterString.Split(_config.SplitCharacter); //default : char = '+'

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

                    if (propType == typeof(string))
                    {
                        //contains or = or exception
                        collection = collection.Where(filter);
                    }
                    else if (propType == typeof(int) || propType == typeof(double) || propType == typeof(long) || propType == typeof(decimal)) //TODO: filter was not tested | maybe need to add else
                    {
                        // > or < or = or exception
                        collection = collection.Where(filter);
                    }
                    else // bool, char e.g.
                    {
                        collection = collection.Where(filter);
                    }
                    //collection = collection.Where($"{propName}.Contains({expr})");

                    filterString += filter + _config.SplitCharacter; //if exception was not invoked
                }
                catch (Exception)
                {
                    //do nothing if property was not found or other
                }
            }

            return collection;
        }

        public IQueryable<T> Pagination<T>(IQueryable<T> collection, ref PageRequestModel requestModel, out PageModel responseModel)
        {
            int collectionCount = 0;

            try
             {
                if (requestModel is null)
                {
                    requestModel = new PageRequestModel();
                    throw new Exception();
                }

                collectionCount = collection.Count();
                collection = collection.Page(requestModel.Page, requestModel.PageSize);
            }
            catch (Exception)
            {
                collection = collection.Page(_config.DefaultPage, _config.DefaultPageSize);
                requestModel.Page = _config.DefaultPage;
                requestModel.PageSize = _config.DefaultPageSize;
            }

            responseModel = new PageModel(collectionCount, requestModel.Page, requestModel.PageSize);

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