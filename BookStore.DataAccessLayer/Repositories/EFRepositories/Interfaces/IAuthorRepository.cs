using BookStore.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces
{
    public interface IAuthorRepository : IEFRepository<Author>
    {
        public Task<List<Author>> FindByIdAsync(List<long> ids);
    }
}
