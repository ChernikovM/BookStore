using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.PrintingEdition;
using BookStore.BusinessLogicLayer.Models.ResponseModel.PrintingEdition;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.PresentationLayer.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrintingEditionController : ICrudController<PrintingEditionModel, PrintingEditionCreateModel>
    {
        private readonly IPrintingEditionService _peService;

        public PrintingEditionController(IPrintingEditionService peService)
        {
            _peService = peService;
        }

        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]PrintingEditionCreateModel model)
        {
            await _peService.CreateAsync(model);

            return new OkObjectResult("success");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _peService.GetAsync(id);

            return new OkObjectResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(IndexRequestModel model)
        {
            var result = await _peService.GetAllAsync(model);

            return new OkObjectResult(result);
        }

        [Authorize("AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) //TODO: dont work
        {
            await _peService.RemoveAsync(id);

            return new OkObjectResult("deleted");
        }

        [Authorize("AdminOnly")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, PrintingEditionModel model)
        {
            await _peService.UpdateAsync(id, model);

            return new OkObjectResult("updated");
        }
    }
}
