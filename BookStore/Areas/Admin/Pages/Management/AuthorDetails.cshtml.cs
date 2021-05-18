using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Author;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Areas.Admin.Pages.Management
{
    [Authorize(Policy="AdminCookiePolicy")]
    public class AuthorDetailsModel : PageModel
    {
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        [Required]
        [BindProperty]
        public long? Id { get; set; }

        [Required]
        [BindProperty]
        public string Name { get; set; }

        private readonly IAuthorService _authorService;

        public AuthorDetailsModel(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            AuthorModel model;

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            try
            {
                model = await _authorService.GetAsync(id);
            }
            catch (CustomException ex)
            {
                ModelState.AddModelError(string.Empty, ex.ErrorMessages.First());
                return Page();
            }

            Name = model.Name;
            Id = model.Id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long id)
        {
            var Author = new AuthorModel();

            Author.Name = Name;
            Author.Id = Id;

            try
            {
                await _authorService.UpdateAsync(id, Author);
            }
            catch (CustomException ex)
            {
                ErrorMessage = ex.ErrorMessages.First();
                return await OnGetAsync(id);
            }

            SuccessMessage = "Author was successfully updated.";
            return Page();
        }
    }
}
