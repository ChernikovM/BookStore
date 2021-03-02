using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.ResponseModel.PrintingEdition;
using System.Collections.Generic;

namespace BookStore.BusinessLogicLayer.Models.ResponseModels.Author
{
    public class AuthorModel : BaseModel
    {
        public string Name { get; set; }

        public List<PrintingEditionModel> PrintingEditions {get; set;}
    }
}
