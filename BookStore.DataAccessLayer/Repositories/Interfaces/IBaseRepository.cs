using BookStore.DataAccessLayer.Entities.Base;
using System;
using System.Collections.Generic;

namespace BookStore.DataAccessLayer.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        void Create(TEntity item);
        TEntity FindById(long id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}
