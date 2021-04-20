using BookStore.DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.RequestModels.Order
{
    public class OrderCreateModel
    {
        public string Description { get; set; }

        public string UserId { get; set; }

        [EnumDataType(typeof(Enums.OrderStatusType))]
        public int Status { get; set; }

        public long? PaymentId { get; set; }
    }
}
