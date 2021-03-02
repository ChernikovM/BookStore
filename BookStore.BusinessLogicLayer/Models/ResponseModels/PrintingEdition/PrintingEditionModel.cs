using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Author;
using System.Collections.Generic;

namespace BookStore.BusinessLogicLayer.Models.ResponseModel.PrintingEdition
{
    public class PrintingEditionModel : BaseModel
    {
        public string Title { get; set; }

        public string Price { get; set; }

        public int Currency { get; set; }

        public int Type { get; set; }

        public string Description { get; set; }

        public List<AuthorModel> Authors { get; set; }
    }
}
