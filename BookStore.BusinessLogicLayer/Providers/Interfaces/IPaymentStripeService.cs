using BookStore.BusinessLogicLayer.Models.RequestModels.Order;
using BookStore.BusinessLogicLayer.Models.RequestModels.Payment;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Payment;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Providers.Interfaces
{
    //TODO: change directory to services/interfaces
    public interface IPaymentStripeService : ICrudService<PaymentModel, PaymentCreationModel>
    {
        Task<CreateSessionResponseModel> CreateSessionAsync(PaymentSessionRequestModel model, string userId);

        Task<PaymentSuccessPageModel> PaymentSuccess(string sessionId, long orderId);
    }
}
