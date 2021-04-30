using AutoMapper;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.OrderItem;
using BookStore.BusinessLogicLayer.Models.ResponseModels.OrderItem.cs;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.PresentationLayer.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderItemsController : ICrudController<OrderItemModel, OrderItemCreateModel>
    {
        private readonly IOrderItemService _orderItemsService;
        private readonly IMapper _mapper;

        public OrderItemsController(
            IOrderItemService orderItemsService,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _orderItemsService = orderItemsService;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Create([FromBody]OrderItemCreateModel model)
        {
            await _orderItemsService.CreateAsync(model);

            return new OkObjectResult("success");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery]long id)
        {
            await _orderItemsService.RemoveAsync(id);

            return new OkObjectResult("deleted");
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromQuery]long id)
        {
            var result = await _orderItemsService.GetAsync(id);

            return new OkObjectResult(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody]IndexRequestModel model)
        {
            var result = await _orderItemsService.GetAllAsync(model);

            return new OkObjectResult(result);
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromQuery]long id, [FromBody]OrderItemModel model)
        {
            await _orderItemsService.UpdateAsync(id, model);

            return new OkObjectResult("updated");
        }
    }
}
