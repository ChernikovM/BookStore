using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Extensions;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.PrintingEdition;
using BookStore.BusinessLogicLayer.Models.ResponseModel.PrintingEdition;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Enums;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static BookStore.BusinessLogicLayer.Constants.Constants;

namespace BookStore.BusinessLogicLayer.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IPrintingEditionRepository _repository;
        private readonly IDataCollectionAccessProvider _dataService;
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;

        public PrintingEditionService(IPrintingEditionRepository repository, IDataCollectionAccessProvider dataService, IMapper mapper, IAuthorRepository authorRepository)
        {
            _repository = repository;
            _dataService = dataService;
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        private async Task<PrintingEdition> FindByIdAsync(long id)
        {
            var entity = await _repository.FindByIdAsync(id);
            if (entity is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, ErrorMessage.PrintingEditionNotFound.GetDescription());
            }

            return entity;
        }

        public async Task<long> CreateAsync(PrintingEditionCreateModel model)
        {
            var authorsIds = model.Authors.Select(x => x.Id.Value).ToList();
            var authors = await _authorRepository.FindByIdAsync(authorsIds);

            if (authors.Count < 1)
            {
                throw new CustomException(HttpStatusCode.BadRequest, ErrorMessage.InvalidData.GetDescription("Authors"));
            }

            var entity = _mapper.Map<PrintingEdition>(model);

            entity.Authors = authors;

            return await _repository.CreateAsync(entity);
        }

        public async Task<PrintingEditionModel> GetAsync(long id)
        {
            var result = await FindByIdAsync(id);

            return _mapper.Map<PrintingEditionModel>(result);
        }

        public async Task<DataCollectionModel<PrintingEditionModel>> GetAllAsync(IndexRequestModel model)
        {
            var result = await _repository.GetAllAsync();

            _dataService.GetCollection(result, model, out DataCollectionModel<PrintingEditionModel> response);

            return response;
        }

        public async Task RemoveAsync(long id)
        {
            var entity = await FindByIdAsync(id);

            entity.Authors = null;
            entity.OrderItems = null;

            await _repository.RemoveAsync(entity);
        }

        public async Task UpdateAsync(long id, PrintingEditionModel model)
        {
            if (id != model.Id)
            {
                throw new CustomException(HttpStatusCode.BadRequest);
            }

            var entity = await FindByIdAsync(id);

            var authors = await _authorRepository.FindByIdAsync(model.Authors.Select(x => x.Id.Value).ToList());
            entity.Authors = authors;

            entity.Currency = model.Currency.Value;
            entity.Description = model.Description;
            entity.Price = model.Price.Value;
            entity.Title = model.Title;
            entity.Type = model.Type.Value;

            await _repository.UpdateAsync(entity);
        }
    }
}
