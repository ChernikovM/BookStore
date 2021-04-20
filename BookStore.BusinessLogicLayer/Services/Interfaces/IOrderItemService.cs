using BookStore.BusinessLogicLayer.Models.RequestModels.OrderItem;
using BookStore.BusinessLogicLayer.Models.ResponseModels.OrderItem.cs;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderItemService : ICrudService<OrderItemModel, OrderItemCreateModel>
    {

    }
}
