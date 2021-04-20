using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.DataAccessLayer.Enums;

namespace BookStore.BusinessLogicLayer.Models.ResponseModels.OrderItem.cs
{
    public class OrderItemModel: BaseModel
    {
        public Enums.PrintingEditionType ProductType { get; set; }

        public string Title { get; set; }

        public int Count { get; set; }

        public decimal Amount { get; set; }
    }
}
