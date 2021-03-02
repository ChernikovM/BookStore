using BookStore.DataAccessLayer.AppContext;
using BookStore.DataAccessLayer.Entities.Base;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataAccessLayer.Repositories.EFRepositories.Base
{
    public class EFRepository<TEntity> : IEFRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly DataContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public EFRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> FindByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> GetAsync(TEntity entity)
        {
            return await _dbSet.FindAsync(entity);
        }

        public virtual Task<IQueryable<TEntity>> GetAllAsync()
        {
            return new Task<IQueryable<TEntity>>(_dbSet.AsQueryable);
        }

        public async Task RemoveAsync(TEntity item)
        {
            item.IsRemoved = true;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
