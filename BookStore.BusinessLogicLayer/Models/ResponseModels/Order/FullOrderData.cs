using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.ResponseModels.OrderItem.cs;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Payment;
using BookStore.BusinessLogicLayer.Models.ResponseModels.User;
using System.Collections.Generic;

namespace BookStore.BusinessLogicLayer.Models.ResponseModels.Order
{
    public class FullOrderData : BaseModel
    {
        public OrderModel Order { get; set; }

        public UserResponseModel User {get; set;}

        public PaymentModel Payment { get; set; }

        public List<OrderItemModel> Items { get; set; }
    }
}
