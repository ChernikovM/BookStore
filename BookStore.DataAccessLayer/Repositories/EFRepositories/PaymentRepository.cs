using BookStore.DataAccessLayer.AppContext;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Base;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataAccessLayer.Repositories.EFRepositories
{
    public class PaymentRepository : EFRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(DataContext context) : base(context)
        {
        }

        public override async Task<IQueryable<Payment>> GetAllAsync()
        {
            var result = _dbSet
                .Where(x => x.IsRemoved == false);

            return result;
        }
    }
}
