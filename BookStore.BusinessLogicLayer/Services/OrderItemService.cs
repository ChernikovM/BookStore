using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Extensions;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.OrderItem;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels.OrderItem.cs;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;
using static BookStore.BusinessLogicLayer.Constants.Constants;

namespace BookStore.BusinessLogicLayer.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IMapper _mapper;
        private readonly IOrderItemRepository _orderItemRepository;
        private IDataCollectionAccessProvider _dataService;

        public OrderItemService(
            IOrderItemRepository orderItemRepository,
            IMapper mapper,
            IDataCollectionAccessProvider dataService
            )
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
            _dataService = dataService;
        }

        private async Task<OrderItem> FindByIdAsync(long id)
        {
            var orderItem = await _orderItemRepository.FindByIdAsync(id);

            if (orderItem is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, ErrorMessage.OrderItemNotFound.GetDescription());
            }

            return orderItem;
        }

        public async Task<long> CreateAsync(OrderItemCreateModel model)
        {
            var item = _mapper.Map<OrderItem>(model);

            return await _orderItemRepository.CreateAsync(item);
        }

        public async Task<DataCollectionModel<OrderItemModel>> GetAllAsync(IndexRequestModel model)
        {
            var query = await _orderItemRepository.GetAllAsync();

            _dataService.GetCollection(query, model, out DataCollectionModel<OrderItemModel> response);

            return response;
        }

        public async Task<OrderItemModel> GetAsync(long id)
        {
            var entity = await FindByIdAsync(id);

            var result = _mapper.Map<OrderItemModel>(entity);

            return result;
        }

        public async Task RemoveAsync(long id)
        {
            var orderItem = await FindByIdAsync(id);

            orderItem.Order = null;
            orderItem.PrintingEdition = null;

            await _orderItemRepository.RemoveAsync(orderItem);
        }

        public async Task UpdateAsync(long id, OrderItemModel model)
        {
            if (id != model.Id)
            {
                throw new CustomException(HttpStatusCode.BadRequest);
            }

            var entity = await FindByIdAsync(id);

            entity.Amount = model.Amount;
            entity.Count = model.Count;

            await _orderItemRepository.UpdateAsync(entity);
        }
    }
}
