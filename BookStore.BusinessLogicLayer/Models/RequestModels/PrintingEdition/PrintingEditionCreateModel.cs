using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.DataAccessLayer.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.RequestModels.PrintingEdition
{
    public class PrintingEditionCreateModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Range(0, double.MaxValue, ErrorMessage = "Invalid price.")]
        public decimal Price { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EnumDataType(typeof(Enums.CurrencyType))]
        public int? Currency { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EnumDataType(typeof(Enums.PrintingEditionType))]
        public int? Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false)]
        public List<BaseModel> Authors { get; set; }
    }
}
