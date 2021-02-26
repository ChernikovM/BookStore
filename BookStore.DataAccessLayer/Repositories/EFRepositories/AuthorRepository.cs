using BookStore.DataAccessLayer.AppContext;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Base;

namespace BookStore.DataAccessLayer.Repositories.EFRepositories
{
    public class AuthorRepository : EFRepository<Author>
    {
        public AuthorRepository(DataContext context) : base(context)
        { 
            
        }
    }
}
