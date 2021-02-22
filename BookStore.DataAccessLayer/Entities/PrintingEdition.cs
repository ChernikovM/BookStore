using BookStore.DataAccessLayer.Entities.Base;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace BookStore.DataAccessLayer.Entities
{
    public class PrintingEdition : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public Enums.Enums.CurrencyType Currency { get; set; }

        public Enums.Enums.PrintingEditionType Type { get; set; }

        [JsonIgnore]
        public List<Author> Authors { get; set; } = new List<Author>();

        [JsonIgnore]
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
