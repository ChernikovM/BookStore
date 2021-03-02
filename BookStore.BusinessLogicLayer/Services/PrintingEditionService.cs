using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.ResponseModel.PrintingEdition;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IPrintingEditionRepository _repository;
        private readonly IDataCollectionAccessService _dataService;
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;

        public PrintingEditionService(IPrintingEditionRepository repository, IDataCollectionAccessService dataService, IMapper mapper, IAuthorRepository authorRepository)
        {
            _repository = repository;
            _dataService = dataService;
            _mapper = mapper;
            _authorRepository = authorRepository;
        }


        public async Task CreateAsync(PrintingEditionModel model)
        {
            var authorsModelsList = model.Authors; //TODO: error when Authors is null
            var authors = new List<Author>();

            foreach (var authorModel in authorsModelsList)
            {
                var author = await _authorRepository.FindByIdAsync(authorModel.Id);
                if (author is null)
                {
                    continue;
                }
                authors.Add(author);
            }

            if (authors.Count < 1)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid Authors.");
            }

            var entity = _mapper.Map<PrintingEdition>(model);

            entity.Authors = authors;

            await _repository.CreateAsync(entity);
        }

        public async Task<PrintingEditionModel> GetAsync(PrintingEditionModel model)
        {
            var result = await _repository.GetAsync(_mapper.Map<PrintingEdition>(model));

            return _mapper.Map<PrintingEditionModel>(result);
        }

        public async Task<DataCollectionModel<PrintingEditionModel>> GetAllAsync(IndexRequestModel model)
        {
            var result = await _repository.GetAllAsync();

            _dataService.GetCollection(result, model, out DataCollectionModel<PrintingEditionModel> response);

            return response;
        }

        public async Task RemoveAsync(PrintingEditionModel model)
        {
            if (model.Id == default)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Ivalid Id.");
            }

            var entity = await _repository.FindByIdAsync(model.Id);
            if (entity is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid data.");
            }

            await _repository.RemoveAsync(entity);
        }

        public async Task UpdateAsync(PrintingEditionModel model)
        {
            if (model.Id == default)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Ivalid Id.");
            }

            var entity = await _repository.FindByIdAsync(model.Id);
            if (entity is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid data.");
            }

            await _repository.UpdateAsync(_mapper.Map<PrintingEdition>(entity));
        }
    }
}
