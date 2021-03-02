using BookStore.DataAccessLayer.AppContext;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Base;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;

namespace BookStore.DataAccessLayer.Repositories.EFRepositories
{
    public class OrderItemRepository : EFRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(DataContext context) : base(context)
        {
        }
    }
}
