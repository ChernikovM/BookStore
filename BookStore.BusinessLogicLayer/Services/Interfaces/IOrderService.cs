using BookStore.BusinessLogicLayer.Models.RequestModels.Order;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Order;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderService : ICrudService<OrderModel, OrderCreateModel>
    {

    }
}
