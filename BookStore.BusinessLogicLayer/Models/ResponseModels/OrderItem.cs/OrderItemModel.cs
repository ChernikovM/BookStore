using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.ResponseModel.PrintingEdition;

namespace BookStore.BusinessLogicLayer.Models.ResponseModels.OrderItem.cs
{
    public class OrderItemModel: BaseModel
    {
        public PrintingEditionModel PrintingEdition { get; set; }

        public int Count { get; set; }

        public decimal Amount { get; set; }
    }
}
