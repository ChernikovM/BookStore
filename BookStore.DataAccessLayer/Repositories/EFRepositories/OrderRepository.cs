using BookStore.DataAccessLayer.AppContext;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Base;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
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
                .Where(x => x.IsRemoved == false);

            return result;
        }
    }
}
