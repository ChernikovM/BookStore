using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models.ResponseModel.PrintingEdition;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Author;
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
    public class ProductDetailsModel : PageModel
    {
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        [Required]
        [BindProperty]
        public long? Id { get; set; }

        [Required]
        [BindProperty]
        public string Authors { get; set; }

        [Required]
        [BindProperty]        
        public string Type { get; set; }

        [Required]
        [BindProperty]        
        public string Currency { get; set; }

        [Required]
        [BindProperty]
        public string Title { get; set; }

        [Required]
        [BindProperty]
        public string Description { get; set; }

        [Required]
        [BindProperty]
        public string Price { get; set; }

        private readonly IPrintingEditionService _peService;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public ProductDetailsModel(IPrintingEditionService peService, IAuthorService authorService, IMapper mapper)
        {
            _peService = peService;
            _authorService = authorService;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            PrintingEditionModel peModel;

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            try
            {
                peModel = await _peService.GetAsync(id);
            }
            catch (CustomException ex)
            {
                ModelState.AddModelError(string.Empty, ex.ErrorMessages.First());
                return Page();
            }

            Title = peModel.Title;
            Description = peModel.Description;
            Price = peModel.Price.ToString();
            Type = peModel.Type.ToString();
            Currency = peModel.Currency.ToString();
            Id = peModel.Id;

            string authors = "";

            bool first = true;
            foreach(var a in peModel.Authors)
            {
                if(first)
                {
                    authors += a.Name;
                    first = false;
                    continue;
                }
                authors += $",{a.Name}";
            }

            Authors = authors;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long id)
        {
            var Product = new PrintingEditionModel();

            Product.Title = Title;
            Product.Description = Description;
            Product.Id = Id;

            if (Price.Contains('.'))
            {
                Price = Price.Replace('.', ',');
            }
                
            if (!decimal.TryParse(Price, out var parsedPrice))
            {
                ErrorMessage = "Incorrect Price";
                return await OnGetAsync(id);
            }

            Product.Price = parsedPrice;

            if (!Enum.TryParse(typeof(Enums.PrintingEditionType), Type, out var parsedType))
            {
                ErrorMessage = "Invalid Type";
                return await OnGetAsync(id);
            }

            Product.Type = (Enums.PrintingEditionType)parsedType;

            if (!Enum.TryParse(typeof(Enums.CurrencyType), Currency, out var parsedCurrency))
            {
                ErrorMessage = "Invalid Currency";
                return await OnGetAsync(id);
            }

            Product.Currency = (Enums.CurrencyType)parsedCurrency;
            
            List<string> authorsNamesList = Authors.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();

            try
            {
                var authors = await _authorService.FindByNames(authorsNamesList);

                Product.Authors = _mapper.Map<List<AuthorModel>>(authors);

                await _peService.UpdateAsync(id, Product);
            }
            catch (CustomException ex)
            {
                ErrorMessage = ex.ErrorMessages.First();
                return await OnGetAsync(id);
            }

            SuccessMessage = "Product was successfully updated.";
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
