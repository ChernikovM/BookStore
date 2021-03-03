using BookStore.BusinessLogicLayer.Models.RequestModels.Author;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Author;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthorService : ICrudService<AuthorModel, AuthorCreateModel>
    {
    }
}
