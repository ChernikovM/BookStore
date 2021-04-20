using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.ResponseModels.Order
{
    public class OrderModel : BaseModel
    {
        public string Description { get; set; }

        [EnumDataType(typeof(Enums.OrderStatusType))]
        public int Status { get; set; }

        public string UserId { get; set; }

        public long PaymentId { get; set; }
    }
}
