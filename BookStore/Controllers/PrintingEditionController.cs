using BookStore.BusinessLogicLayer.Models.Base;
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
        public async Task<IActionResult> Create(PrintingEditionCreateModel model)
        {
            await _peService.CreateAsync(model);
            
            return new OkObjectResult("success");
        }

        [HttpGet]
        public async Task<IActionResult> Get(BaseModel model)
        {
            var result = await _peService.GetAsync(model);

            return new OkObjectResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(IndexRequestModel model)
        {
            var result = await _peService.GetAllAsync(model);

            return new OkObjectResult(result);
        }

        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Remove(BaseModel model)
        {
            await _peService.RemoveAsync(model);

            return new OkObjectResult("removed");
        }

        [Authorize("AdminOnly")]
        [HttpPost]
        //[Route("{id}/update")]
        public async Task<IActionResult> Update(PrintingEditionModel model)
        {
            await _peService.UpdateAsync( model);

            return new OkObjectResult("updated");
        }
    }
}
