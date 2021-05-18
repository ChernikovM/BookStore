using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.RequestModels.PrintingEdition;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Areas.Admin.Pages.Management
{
    [Authorize(Policy = "AdminCookiePolicy")]
    public class CreateProductModel : PageModel
    {
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        [BindProperty]
        [Required]
        public string Authors { get; set; }

        [BindProperty]
        [Required]
        public string Type { get; set; }

        [BindProperty]
        [Required]
        public string Currency { get; set; }

        [BindProperty]
        [Required]
        public string Title { get; set; }

        [BindProperty]
        [Required]
        public string Description { get; set; }

        [BindProperty]
        [Required]
        public string Price { get; set; }

        private readonly IAuthorService _authorService;

        private readonly IPrintingEditionService _peService;

        public CreateProductModel(IAuthorService authorsService, IPrintingEditionService peService)
        {
            _authorService = authorsService;
            _peService = peService;
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
            var Product = new PrintingEditionCreateModel();

            Product.Title = Title;
            Product.Description = Description;
            try
            {
                if (Price.Contains('.'))
                {
                    Price = Price.Replace('.', ',');
                }
                Product.Price = decimal.Parse(Price);
            }
            catch(Exception ex)
            {
                ErrorMessage = "Incorrect Price";
                return await OnGetAsync();
            }

            var type = Enum.Parse(typeof(Enums.PrintingEditionType), Type);
            var currency = Enum.Parse(typeof(Enums.CurrencyType), Currency);

            Product.Currency = (int)currency;
            Product.Type = (int)type;

            List<string> authorsNamesList = Authors.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();

            try
            {
                var authors = await _authorService.FindByNames(authorsNamesList);
                var list = new List<BaseModel>();
                foreach(var a in authors)
                {
                    list.Add(new BaseModel() { Id = a.Id });
                }
                Product.Authors = list;

                await _peService.CreateAsync(Product);
            }
            catch(CustomException ex)
            {
                ErrorMessage = ex.ErrorMessages.First();
                return await OnGetAsync();
            }

            SuccessMessage = "Product was successfully created.";
            return Page();
        }

        public SelectList GetTypesList()
        {
            return new SelectList(Enum.GetValues(typeof(Enums.PrintingEditionType)));
        }

        public SelectList GetCurrencyTypes()
        {
            return new SelectList(Enum.GetValues(typeof(Enums.CurrencyType)));
        }
    }
}
