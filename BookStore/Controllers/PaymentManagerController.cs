using BookStore.BusinessLogicLayer.Models.RequestModels.Order;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentManagerController : Controller
    {
        private readonly IPaymentStripeService _paymentService;

        public PaymentManagerController(IPaymentStripeService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [Authorize]
        public async Task<OkObjectResult> CreatePaymentSession([FromBody]PaymentSessionRequestModel model)
        {
            HttpContext.Request.Headers.TryGetValue("Authorization", out var value);
            var accessToken = value.ToString().Split(" ").Last();

            var result = await _paymentService.CreateSessionAsync(model, accessToken);

            return new OkObjectResult(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<OkObjectResult> PaymentSuccess([FromQuery] string sessionId, [FromQuery] long orderId)
        {
            var result = await _paymentService.PaymentSuccess(sessionId, orderId);

            return new OkObjectResult(result);
        }
    }
}
