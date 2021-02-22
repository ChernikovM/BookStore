using BookStore.DataAccessLayer.Entities.Base;
using Newtonsoft.Json;

namespace BookStore.DataAccessLayer.Entities
{
    public class OrderItem : BaseEntity
    {
        public decimal Amount { get; set; }

        public Enums.Enums.CurrencyType Currency { get; set; }

        public int Count { get; set; }

        public long OrderId { get; set; }

        public long PrintingEditionId { get; set; }

        [JsonIgnore]
        public PrintingEdition PrintingEdition { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }
    }
}
