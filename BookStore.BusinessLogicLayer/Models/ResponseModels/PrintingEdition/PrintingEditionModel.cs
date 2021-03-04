using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Author;
using BookStore.DataAccessLayer.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.ResponseModel.PrintingEdition
{
    public class PrintingEditionModel : BaseModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Invalid price.")]
        public decimal? Price { get; set; }
        
        [Required]
        [EnumDataType(typeof(Enums.CurrencyType))]
        public Enums.CurrencyType? Currency { get; set; }

        [Required]
        [EnumDataType(typeof(Enums.PrintingEditionType))]
        public Enums.PrintingEditionType? Type { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public List<AuthorModel> Authors { get; set; }
    }
}
