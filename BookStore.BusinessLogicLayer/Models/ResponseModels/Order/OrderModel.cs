using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.ResponseModels.OrderItem.cs;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Payment;
using BookStore.BusinessLogicLayer.Models.ResponseModels.User;
using BookStore.DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.ResponseModels.Order
{
    public class OrderModel : BaseModel
    {
        public string Description { get; set; }

        [EnumDataType(typeof(Enums.OrderStatusType))]
        public Enums.OrderStatusType Status { get; set; }

        public string UserId { get; set; }

        public UserResponseModel User { get; set; }

        public long PaymentId { get; set; }

        public PaymentModel Payment { get; set; }

        public List<OrderItemModel> OrderItems { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
