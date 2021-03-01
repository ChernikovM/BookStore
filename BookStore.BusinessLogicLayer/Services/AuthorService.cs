using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models;
using BookStore.BusinessLogicLayer.Models.Author;
using BookStore.BusinessLogicLayer.Models.Responses;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using System.Net;

namespace BookStore.BusinessLogicLayer.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IDataCollectionAccessService _dataService;
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorService(IDataCollectionAccessService dataService, IAuthorRepository repository, IMapper mapper)
        {
            _dataService = dataService;
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(AuthorModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid author's name.");
            }

            var entity = _mapper.Map<Author>(model);
            
            _repository.Create(entity);
        }

        public AuthorModel Get(AuthorModel model)
        {
            if (model.Id == default(long))
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid author's id.");
            }

            var ent = _mapper.Map<Author>(model);

            var entity = _repository.Get(ent); //TODO: loop mapper
            //var entity = _repository.FindById(model.Id);

            return _mapper.Map<AuthorModel>(entity);
        }

        public DataCollectionModel<AuthorModel> GetAll(IndexRequestModel model)
        {
            var query = _repository.GetAll();

            _dataService.GetCollection<AuthorModel, Author>(query, model, out DataCollectionModel<AuthorModel> response);

            

            return response;
        }

        public void Remove(AuthorModel model)
        {
            if (model.Id == default(long))
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid author's id.");
            }

            var author = _repository.FindById(model.Id);
            _repository.Remove(author);
        }

        public void Update(AuthorModel model)
        {
            if (model.Id == default(long))
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid author's id.");
            }

            var author = _repository.FindById(model.Id);
            
            _repository.Update(author);
        }

    }

}

