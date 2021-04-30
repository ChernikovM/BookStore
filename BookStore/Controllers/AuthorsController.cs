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
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ICrudController<AuthorModel, AuthorCreateModel>
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AuthorCreateModel model)
        {
            await _authorService.CreateAsync(model);

            return new OkObjectResult("Author was successfully added.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _authorService.GetAsync(id);

            return new OkObjectResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromBody]IndexRequestModel model)
        {
            var result = await _authorService.GetAllAsync(model);

            return new OkObjectResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _authorService.RemoveAsync(id);

            return new OkObjectResult("Author was successfully removed.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody]AuthorModel model)
        {
            await _authorService.UpdateAsync(id, model);

            return new OkObjectResult("Author was successfully updated.");
        }
    }
}
