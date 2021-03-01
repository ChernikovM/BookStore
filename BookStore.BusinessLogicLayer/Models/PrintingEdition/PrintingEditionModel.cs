using BookStore.BusinessLogicLayer.Models.Author;
using BookStore.BusinessLogicLayer.Models.Base;
using System.Collections.Generic;

namespace BookStore.BusinessLogicLayer.Models.PrintingEdition
{
    public class PrintingEditionModel : BaseModel
    {
        public string Title { get; set; }

        public string Price { get; set; }

        public int Currency { get; set; }

        public int Type { get; set; }

        public string Description { get; set; }

        public long Id { get; set; }

        public List<AuthorModel> Authors { get; set; }
    }
}
