using BookStore.DataAccessLayer.AppContext;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Base;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataAccessLayer.Repositories.EFRepositories
{
    public class AuthorRepository : EFRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(DataContext context) : base(context)
        { 
            
        }

        public override async Task<Author> GetAsync(Author entity)
        {
            var result = await _dbSet
                .Include(x => x.PrintingEditions)
                .FirstOrDefaultAsync(x => x.Id == entity.Id && x.IsRemoved == false);

            return result;
        }

        public override async Task<IQueryable<Author>> GetAllAsync()
        {
            var result = _dbSet
                .Include(x => x.PrintingEditions)
                .Where(x => x.IsRemoved == false);

            return result;
        }
    }
}
