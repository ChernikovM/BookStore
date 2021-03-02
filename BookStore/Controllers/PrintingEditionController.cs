using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.ResponseModel.PrintingEdition;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.PresentationLayer.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PrintingEditionController : ICrudController<PrintingEditionModel>
    {
        private readonly IPrintingEditionService _peService;

        public PrintingEditionController(IPrintingEditionService peService)
        {
            _peService = peService;
        }

        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create(PrintingEditionModel model)
        {
            await _peService.CreateAsync(model);
            
            return new OkObjectResult("success");
        }

        public async Task<IActionResult> Get(PrintingEditionModel model)
        {
            var result = await _peService.GetAsync(model);

            return new OkObjectResult(result);
        }

        public async Task<IActionResult> GetAll(IndexRequestModel model)
        {
            var result = await _peService.GetAllAsync(model);

            return new OkObjectResult(result);
        }

        public async Task<IActionResult> Remove(PrintingEditionModel model)
        {
            await _peService.RemoveAsync(model);

            return new OkObjectResult("removed");
        }

        public async Task<IActionResult> Update(PrintingEditionModel model)
        {
            await _peService.RemoveAsync(model);

            return new OkObjectResult("updated");
        }
    }
}
