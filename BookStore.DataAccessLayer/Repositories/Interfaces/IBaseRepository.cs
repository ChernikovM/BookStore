using BookStore.DataAccessLayer.Entities.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataAccessLayer.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task CreateAsync(TEntity item);
        Task<TEntity> FindByIdAsync(long id);
        Task<List<TEntity>> FindByIdAsync(List<long> ids);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task RemoveAsync(TEntity item);
        Task UpdateAsync(TEntity item);
        Task SaveChangesAsync();
    }
}
