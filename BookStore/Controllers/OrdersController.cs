using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.Order;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Order;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.PresentationLayer.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ICrudController<OrderModel, OrderCreateModel>
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Create(OrderCreateModel model)
        {
            await _orderService.CreateAsync(model);

            return new ObjectResult("success");
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _orderService.GetAsync(id);

            return new OkObjectResult(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _orderService.RemoveAsync(id);

            return new OkObjectResult("deleted");
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetAll(IndexRequestModel model)
        {
            var result = await _orderService.GetAllAsync(model);

            return new OkObjectResult(result);
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(long id, OrderModel model)
        {
            await _orderService.UpdateAsync(id, model);

            return new OkObjectResult("updated");
        }
    }
}
