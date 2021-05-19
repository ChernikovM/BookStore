using BookStore.DataAccessLayer.AppContext;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Base;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataAccessLayer.Repositories.EFRepositories
{
    public class OrderRepository : EFRepository<Order>, IOrderRepository
    {
        public OrderRepository(DataContext context) : base(context)
        {
        }

        public override async Task<IQueryable<Order>> GetAllAsync()
        {
            var result = _dbSet
                .Include(x => x.User)
                .Include(x => x.OrderItems).ThenInclude(x => x.PrintingEdition)
                .Include(x => x.Payment)
                .Where(x => x.IsRemoved == false);

            var a = result.ToList();

            return result;
        }
    }
}
