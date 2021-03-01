using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.PrintingEdition;
using System.Collections.Generic;

namespace BookStore.BusinessLogicLayer.Models.Author
{
    public class AuthorModel : BaseModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<PrintingEditionModel> PrintingEditions {get; set;}
    }
}
