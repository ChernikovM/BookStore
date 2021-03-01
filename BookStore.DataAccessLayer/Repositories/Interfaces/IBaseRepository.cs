using BookStore.DataAccessLayer.Entities.Base;
using System;
using System.Linq;

namespace BookStore.DataAccessLayer.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        void Create(TEntity item);
        TEntity FindById(long id);
        IQueryable<TEntity> GetAll();
        TEntity Get(TEntity entity);
        void Remove(TEntity item);
        void Update(TEntity item);
        TEntity FindBy(Func<TEntity, bool> predicate);
    }
}
