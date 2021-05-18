using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Extensions;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.Author;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Author;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using static BookStore.BusinessLogicLayer.Constants.Constants;

namespace BookStore.BusinessLogicLayer.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IDataCollectionAccessProvider _dataService;
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorService(IDataCollectionAccessProvider dataService, IAuthorRepository repository, IMapper mapper)
        {
            _dataService = dataService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Author>> FindByNames(List<string> names)
        {
            var authors = await _repository.FindByNameAsync(names);

            if (authors is null || authors.Count != names.Count)
            {
                throw new CustomException(HttpStatusCode.BadRequest, ErrorMessage.AuthorNotFound.GetDescription());
            }

            return authors;
        }

        private async Task<Author> FindByIdAsync(long id)
        {
            var author = await _repository.FindByIdAsync(id);

            if (author is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, ErrorMessage.AuthorNotFound.GetDescription());
            }

            return author;
        }

        public async Task<long> CreateAsync(AuthorCreateModel model)
        {
            var entity = _mapper.Map<Author>(model);
            
            return await _repository.CreateAsync(entity);
        }

        public async Task<AuthorModel> GetAsync(long id)
        {
            var entity = await FindByIdAsync(id);

            var result= _mapper.Map<AuthorModel>(entity);

            return result;
        }

        public async Task<DataCollectionModel<AuthorModel>> GetAllAsync(IndexRequestModel model)
        {
            var query = await _repository.GetAllAsync();

            _dataService.GetCollection(query, model, out DataCollectionModel<AuthorModel> response);

            return response;
        }

        public async Task RemoveAsync(long id)
        {
            var author = await FindByIdAsync(id);

            author.PrintingEditions = null;

            await _repository.RemoveAsync(author);
        }

        public async Task UpdateAsync(long id, AuthorModel model)
        {
            if (id != model.Id)
            {
                throw new CustomException(HttpStatusCode.BadRequest);
            }

            var entity = await FindByIdAsync(id);

            entity.Name = model.Name;

            await _repository.UpdateAsync(entity);
        }

    }

}

