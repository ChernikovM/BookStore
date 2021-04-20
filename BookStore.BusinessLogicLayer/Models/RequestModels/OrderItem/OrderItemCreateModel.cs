using BookStore.DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.RequestModels.OrderItem
{
    public class OrderItemCreateModel
    {
        public long OrderId { get; set; }

        public long PrintingEditionId { get; set; }

        public int Count { get; set; }

        [EnumDataType(typeof(Enums.CurrencyType))]
        public int Currency { get; set; }

        public decimal Amount { get; set; }
    }
}
