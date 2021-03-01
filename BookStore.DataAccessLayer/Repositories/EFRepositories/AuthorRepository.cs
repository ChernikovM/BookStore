using BookStore.DataAccessLayer.AppContext;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Base;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccessLayer.Repositories.EFRepositories
{
    public class AuthorRepository : EFRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(DataContext context) : base(context)
        { 
            
        }

        public override Author Get(Author entity)
        {
            var result = _dbSet
                .Include(x => x.PrintingEditions)
                .FirstAsync( x => x.Id == entity.Id)
                .Result;

            return result;
        }

        public Author FindByName(string name)
        {
            return FindBy(x => x.Name.Equals(name));
        }
    }
}
