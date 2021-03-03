using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.ResponseModel.PrintingEdition;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.ResponseModels.Author
{
    public class AuthorModel : BaseModel
    {
        [Required]
        public string Name { get; set; }

        public List<PrintingEditionModel> PrintingEditions {get; set;}
    }
}
