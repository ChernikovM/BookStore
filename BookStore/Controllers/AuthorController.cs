using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.Author;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Author;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.PresentationLayer.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthorController : ICrudController<AuthorModel, AuthorCreateModel>
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create(AuthorCreateModel model)
        {
            await _authorService.CreateAsync(model);

            return new OkObjectResult("Author was successfully added.");
        }

        [Authorize("AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> Get(BaseModel model)
        {
            var result = await _authorService.GetAsync(model);

            return new OkObjectResult(result);
        }

        [Authorize("AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAll(IndexRequestModel model)
        {
            var result = await _authorService.GetAllAsync(model);

            return new OkObjectResult(result);
        }

        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Remove(BaseModel model)
        {
            await _authorService.RemoveAsync(model);

            return new OkObjectResult("Author was successfully removed.");
        }

        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Update(AuthorModel model)
        {
            await _authorService.UpdateAsync(model);

            return new OkObjectResult("Author was successfully updated.");
        }
    }
}
