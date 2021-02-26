using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Base;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthorContoller
    {
        private readonly EFRepository<Author> _repository;

        public AuthorContoller(EFRepository<Author> repository)
        {
            _repository = repository;
        }



    }
}
