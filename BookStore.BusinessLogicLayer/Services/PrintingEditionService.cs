using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.PrintingEdition;
using BookStore.BusinessLogicLayer.Models.ResponseModel.PrintingEdition;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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
                throw new CustomException(HttpStatusCode.BadRequest, "Not found.");
            }

            return entity;
        }

        public async Task CreateAsync(PrintingEditionCreateModel model)
        {
            var authorsIds = model.Authors.Select(x => x.Id.Value).ToList();
            var authors = await _authorRepository.FindByIdAsync(authorsIds);

            if (authors.Count < 1)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid Authors.");
            }

            var entity = _mapper.Map<PrintingEdition>(model);

            entity.Authors = authors;

            await _repository.CreateAsync(entity);
        }

        public async Task<PrintingEditionModel> GetAsync(BaseModel model)
        {
            var result = await _repository.GetAsync(_mapper.Map<PrintingEdition>(model));

            if (result is null)
            {
                throw new CustomException(HttpStatusCode.NotFound, "Not found.");
            }

            return _mapper.Map<PrintingEditionModel>(result);
        }

        public async Task<DataCollectionModel<PrintingEditionModel>> GetAllAsync(IndexRequestModel model)
        {
            var result = await _repository.GetAllAsync();

            _dataService.GetCollection(result, model, out DataCollectionModel<PrintingEditionModel> response);

            return response;
        }

        public async Task RemoveAsync(BaseModel model)
        {
            var entity = await FindByIdAsync(model.Id.Value);

            await _repository.RemoveAsync(entity);
        }

        public async Task UpdateAsync(PrintingEditionModel model)
        {
            var entity = await FindByIdAsync(model.Id.Value);

            var obj = _mapper.Map<PrintingEdition>(model);

            entity = obj;

            await _repository.UpdateAsync(entity);
        }
    }
}
