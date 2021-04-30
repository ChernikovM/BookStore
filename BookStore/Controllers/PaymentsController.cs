using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.Payment;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Payment;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
using BookStore.PresentationLayer.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : Controller, ICrudController<PaymentModel, PaymentCreationModel>
    {
        private readonly IPaymentStripeService _paymentService;

        public PaymentsController(IPaymentStripeService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPut]
        public async Task<IActionResult> Create([FromBody]PaymentCreationModel model)
        {
            await _paymentService.CreateAsync(model);

            return new OkObjectResult("success");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _paymentService.RemoveAsync(id);

            return new OkObjectResult("deleted");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _paymentService.GetAsync(id);

            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody]IndexRequestModel model)
        {
            var result = await _paymentService.GetAllAsync(model);

            return new OkObjectResult(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromQuery]long id, [FromBody]PaymentModel model)
        {
            await _paymentService.UpdateAsync(id, model);

            return new OkObjectResult("updated");
        }
    }
}
