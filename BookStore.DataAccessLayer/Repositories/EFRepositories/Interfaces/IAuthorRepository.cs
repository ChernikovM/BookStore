using BookStore.DataAccessLayer.Entities;

namespace BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces
{
    public interface IAuthorRepository : IEFRepository<Author>
    {
        Author FindByName(string name);
    }
}
