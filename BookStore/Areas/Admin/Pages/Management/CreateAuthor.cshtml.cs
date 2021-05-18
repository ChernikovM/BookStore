using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models.RequestModels.Author;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Areas.Admin.Pages.Management
{
    [Authorize(Policy = "AdminCookiePolicy")]
    public class CreateAuthorModel : PageModel
    {
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        [BindProperty]
        [Required]
        public string Name { get; set; }

        private readonly IAuthorService _authorService;

        public CreateAuthorModel(IAuthorService authorsService, IPrintingEditionService peService)
        {
            _authorService = authorsService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var Author = new AuthorCreateModel();

            Author.Name = Name;
            
            try
            {
                await _authorService.CreateAsync(Author);
            }
            catch (CustomException ex)
            {
                ErrorMessage = ex.ErrorMessages.First();
                return await OnGetAsync();
            }

            SuccessMessage = "Author was successfully created.";
            return Page();
        }
    }
}
