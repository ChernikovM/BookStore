﻿using BookStore.DataAccessLayer.AppContext;
using BookStore.DataAccessLayer.Entities.Base;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<long> CreateAsync(TEntity item)
        {
            var res = await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
            return res.Entity.Id;
        }

        public virtual async Task<TEntity> FindByIdAsync(long id)
        {
            var result = await _dbSet.FindAsync(id);

            if(result is null || result.IsRemoved)
            {
                return null;
            }

            return result;
        }

        public virtual async Task<List<TEntity>> FindByIdAsync(List<long> ids)
        {
            var result = _dbSet.Where(x => ids.Contains(x.Id) && x.IsRemoved == false);

            return await result.ToListAsync();
        }

        public async virtual Task<IQueryable<TEntity>> GetAllAsync()
        {
            return _dbSet.AsQueryable();
            //return new Task<IQueryable<TEntity>>(_dbSet.AsQueryable);
        }

        public async Task RemoveAsync(TEntity item)
        {
            item.IsRemoved = true;
            _dbSet.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity item)
        { 
            _context.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
