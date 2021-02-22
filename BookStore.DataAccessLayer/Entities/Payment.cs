using BookStore.DataAccessLayer.Entities.Base;
using Newtonsoft.Json;

namespace BookStore.DataAccessLayer.Entities
{
    public class Payment : BaseEntity
    {
        public long TransactionId { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }
    }
}
