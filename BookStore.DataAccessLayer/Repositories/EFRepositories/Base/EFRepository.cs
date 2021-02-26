using BookStore.DataAccessLayer.AppContext;
using BookStore.DataAccessLayer.Entities.Base;
using BookStore.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore.DataAccessLayer.Repositories.EFRepositories.Base
{
    public class EFRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly DataContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public EFRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void Create(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public TEntity FindById(long id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public void Remove(TEntity item)
        {
            item.IsRemoved = true;
            _context.SaveChanges();
        }

        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
