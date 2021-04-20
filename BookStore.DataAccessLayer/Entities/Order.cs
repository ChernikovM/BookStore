using BookStore.DataAccessLayer.Entities.Base;
using System.Collections.Generic;

namespace BookStore.DataAccessLayer.Entities
{
    public class Order : BaseEntity
    {
        
        public string Description { get; set; }

        public Enums.Enums.OrderStatusType Status { get; set; }

        public string UserId { get; set; }

        public long? PaymentId { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public Payment Payment { get; set; }

        public User User { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
