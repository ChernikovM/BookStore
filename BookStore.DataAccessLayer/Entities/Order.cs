using BookStore.DataAccessLayer.Entities.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookStore.DataAccessLayer.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }

        public Enums.Enums.OrderStatusType Status { get; set; }

        public string UserId { get; set; }

        public long PaymentId { get; set; }


        [JsonIgnore]
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        [JsonIgnore]
        public Payment Payment { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
