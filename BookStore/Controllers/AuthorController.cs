using BookStore.BusinessLogicLayer.Models;
using BookStore.BusinessLogicLayer.Models.Author;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.PresentationLayer.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthorController : ICrudController<AuthorModel>
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create(AuthorModel model)
        {
            _authorService.Create(model);

            return new OkObjectResult("Author was successfully added.");
        }

        [Authorize("AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> Get(AuthorModel model)
        {
            var result = _authorService.Get(model);

            return new OkObjectResult(result);
        }

        [Authorize("AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAll(IndexRequestModel model)
        {
            var result = _authorService.GetAll(model);

            return new OkObjectResult(result);
        }

        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Remove(AuthorModel model)
        {
            _authorService.Remove(model);

            return new OkObjectResult("Author was successfully removed.");
        }

        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Update(AuthorModel model)
        {
            _authorService.Update(model);

            return new OkObjectResult("Author was successfully updated.");
        }
    }
}
