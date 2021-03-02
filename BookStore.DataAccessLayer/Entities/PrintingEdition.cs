using BookStore.DataAccessLayer.Entities.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookStore.DataAccessLayer.Entities
{
    public class PrintingEdition : BaseEntity
    {
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public Enums.Enums.CurrencyType Currency { get; set; }

        public Enums.Enums.PrintingEditionType Type { get; set; }

        public List<Author> Authors { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public PrintingEdition()
        {
            Authors = new List<Author>();
            OrderItems = new List<OrderItem>();
        }
    }
}
