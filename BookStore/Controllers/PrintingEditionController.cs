using BookStore.BusinessLogicLayer.Models;
using BookStore.BusinessLogicLayer.Models.PrintingEdition;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.PresentationLayer.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
            _peService.Create(model);

            return new OkObjectResult("success");
        }

        public Task<IActionResult> Get(PrintingEditionModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetAll(IndexRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Remove(PrintingEditionModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Update(PrintingEditionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
