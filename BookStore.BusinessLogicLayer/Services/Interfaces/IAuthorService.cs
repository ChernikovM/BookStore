using BookStore.BusinessLogicLayer.Models.RequestModels.Author;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Author;
using BookStore.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthorService : ICrudService<AuthorModel, AuthorCreateModel>
    {
        Task<List<Author>> FindByNames(List<string> names);
    }
}
