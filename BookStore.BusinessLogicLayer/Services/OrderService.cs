using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Extensions;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.Order;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Order;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Enums;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;
using static BookStore.BusinessLogicLayer.Constants.Constants;

namespace BookStore.BusinessLogicLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private IDataCollectionAccessProvider _dataService;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IDataCollectionAccessProvider dataService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _dataService = dataService;
        }

        private async Task<Order> FindByIdAsync(long id)
        {
            var order = await _orderRepository.FindByIdAsync(id);

            if (order is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, ErrorMessage.OrderNotFound.GetDescription());
            }

            return order;
        }

        public async Task<long> CreateAsync(OrderCreateModel model)
        {
            var entity = _mapper.Map<Order>(model);

            return await _orderRepository.CreateAsync(entity);
        }

        public async Task<DataCollectionModel<OrderModel>> GetAllAsync(IndexRequestModel model)
        {
            var query = await _orderRepository.GetAllAsync();

            _dataService.GetCollection(query, model, out DataCollectionModel<OrderModel> response);

            return response;
        }

        public async Task<OrderModel> GetAsync(long id)
        {
            var entity = await FindByIdAsync(id);

            var result = _mapper.Map<OrderModel>(entity);

            return result;
        }

        public async Task RemoveAsync(long id)
        {
            var order = await FindByIdAsync(id);

            order.UserId = null;
            order.Payment = null;
            order.PaymentId = null;
            order.UserId = null;

            await _orderRepository.RemoveAsync(order);
        }

        public async Task UpdateAsync(long id, OrderModel model)
        {
            if (id != model.Id)
            {
                throw new CustomException(HttpStatusCode.BadRequest);
            }

            var entity = await FindByIdAsync(id);

            entity.Description = model.Description;
            entity.PaymentId = model.PaymentId;
            entity.Status = (Enums.OrderStatusType)model.Status;

            await _orderRepository.UpdateAsync(entity);
        }
    }
}
