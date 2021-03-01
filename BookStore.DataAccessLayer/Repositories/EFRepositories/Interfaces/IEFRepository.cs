using BookStore.DataAccessLayer.Entities.Base;
using BookStore.DataAccessLayer.Repositories.Interfaces;

namespace BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces
{
    public interface IEFRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity: BaseEntity
    {

    }
}
