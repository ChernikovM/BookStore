using AutoMapper;
using BookStore.BusinessLogicLayer.Configurations.Interfaces;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Extensions;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.Order;
using BookStore.BusinessLogicLayer.Models.RequestModels.OrderItem;
using BookStore.BusinessLogicLayer.Models.RequestModels.Payment;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Payment;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Enums;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static BookStore.BusinessLogicLayer.Constants.Constants;

namespace BookStore.BusinessLogicLayer.Providers
{
    //TODO: change directory to Services
    public class PaymentStripeService: IPaymentStripeService
    {
        private readonly IStripeConfiguration _config;
        private readonly IPrintingEditionRepository _peRepository;
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService;
        private readonly IOrderItemService _orderItemService;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IDataCollectionAccessProvider _dataService;

        public PaymentStripeService(
            IStripeConfiguration config,
            IPrintingEditionRepository peRepository,
            IOrderService orderService,
            IAccountService accountService,
            IOrderItemService orderItemService,
            IPaymentRepository paymentRepository,
            IMapper mapper,
            IDataCollectionAccessProvider dataService
            )
        {
            _config = config;
            _peRepository = peRepository;
            _orderService = orderService;
            _accountService = accountService;
            _orderItemService = orderItemService;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _dataService = dataService;
            StripeConfiguration.ApiKey = _config.PrivateKey;
        }

        public async Task<CreateSessionResponseModel> CreateSessionAsync(PaymentSessionRequestModel model, string accessToken) {
            var user = await _accountService.FindByTokenAsync(accessToken);
            
            var orderId = await _orderService.CreateAsync(new OrderCreateModel()
            {
                UserId = user.Id,
                Status = (int)Enums.OrderStatusType.Unpaid
            });

            var itemsList = await CreateItemsListAsync(model.Items, model.Currency, orderId);

            var options = new Stripe.Checkout.SessionCreateOptions()
            {
                SuccessUrl = model.SuccessUrl + "?session_id={CHECKOUT_SESSION_ID}" + $"&order_id={orderId}",
                CancelUrl = model.CancelUrl,
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",
                LineItems = itemsList
            };

            var service = new SessionService();
            var session = service.Create(options);

            var paymentId = await CreateAsync(new PaymentCreationModel()
            {
                SessionId = session.Id
            });

            var order = await _orderService.GetAsync(orderId);
            order.PaymentId = paymentId;
            await _orderService.UpdateAsync(orderId, order);

            return new CreateSessionResponseModel() { SessionId = session.Id };
        }

        private async Task<List<SessionLineItemOptions>> CreateItemsListAsync(List<Item> items, int currencyId, long orderId)
        {
            var itemsList = new List<SessionLineItemOptions>();

            List<long> ids = items.Select(x => x.Id).ToList();

            List<PrintingEdition> peList = await _peRepository.FindByIdAsync(ids);

            foreach (var item in items)
            {
                decimal unitAmount = peList.First(x => x.Id == item.Id).Price;

                itemsList.Add(new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = peList.First(x => x.Id == item.Id).Title
                        },

                        Currency = ((Enums.CurrencyType)currencyId).ToString().ToLower(),

                        //TODO: need to check 
                        //if(pe.currency != order.currency) 
                        //  { price = currencyConverter.Convert(price, fromCurrency, toCurrency) };
                        UnitAmount = (long)(unitAmount * 100),
                    },
                    Quantity = item.Quantity,
                }); ;

                //TODO: convert amount if need
                await _orderItemService.CreateAsync(new OrderItemCreateModel() { 
                    PrintingEditionId = item.Id,
                    Count = item.Quantity,
                    Amount = unitAmount * item.Quantity,
                    OrderId = orderId,
                    Currency = currencyId,
                });
            }

            return itemsList;
        }

        public async Task<PaymentSuccessPageModel> PaymentSuccess(string sessionId, long orderId)
        {
            var sessionService = new SessionService();
            Session session = await sessionService.GetAsync(sessionId);

            var order = await _orderService.GetAsync(orderId);

            if(session.PaymentStatus == "paid")
            {
                order.Status = Enums.OrderStatusType.Paid;
                await _orderService.UpdateAsync(orderId, order);
            }

            var result = new PaymentSuccessPageModel();

            result.OrderId = orderId;

            return result;
        }

        private async Task<Payment> FindByIdAsync(long id)
        {
            var order = await _paymentRepository.FindByIdAsync(id);

            if (order is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, ErrorMessage.PaymentNotFound.GetDescription());
            }

            return order;
        }

        public async Task<PaymentModel> GetAsync(long id)
        {
            var entity = await FindByIdAsync(id);

            var result = _mapper.Map<PaymentModel>(entity);

            return result;
        }

        public async Task<DataCollectionModel<PaymentModel>> GetAllAsync(IndexRequestModel model)
        {
            var query = await _paymentRepository.GetAllAsync();

            _dataService.GetCollection(query, model, out DataCollectionModel<PaymentModel> response);

            return response;
        }

        public async Task<long> CreateAsync(PaymentCreationModel model)
        {
            var item = _mapper.Map<Payment>(model);

            return await _paymentRepository.CreateAsync(item);
        }

        public async Task RemoveAsync(long id)
        {
            var payment = await FindByIdAsync(id);

            payment.Order = null;

            await _paymentRepository.RemoveAsync(payment);
        }

        public async Task UpdateAsync(long id, PaymentModel model)
        {
            if (id != model.Id)
            {
                throw new CustomException(HttpStatusCode.BadRequest);
            }

            var entity = await FindByIdAsync(id);

            entity.SessionId = model.SessionId;

            await _paymentRepository.UpdateAsync(entity);
        }
    }
}
