﻿using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.Author;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Author;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using System.Net;
using System.Threading.Tasks;

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

        private async Task<Author> FindByIdAsync(long id)
        {
            var author = await _repository.FindByIdAsync(id);

            if (author is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Author was not found.");
            }

            return author;
        }

        public async Task CreateAsync(AuthorCreateModel model)
        {
            var entity = _mapper.Map<Author>(model);
            
            await _repository.CreateAsync(entity);
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

            _dataService.GetCollection<AuthorModel, Author>(query, model, out DataCollectionModel<AuthorModel> response);

            return response;
        }

        public async Task RemoveAsync(long id)
        {
            var author = await FindByIdAsync(id);

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

