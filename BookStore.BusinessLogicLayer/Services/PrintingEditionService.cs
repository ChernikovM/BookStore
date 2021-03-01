using AutoMapper;
using BookStore.BusinessLogicLayer.Models;
using BookStore.BusinessLogicLayer.Models.PrintingEdition;
using BookStore.BusinessLogicLayer.Models.Responses;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using System;
using System.Collections.Generic;

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

        public void Create(PrintingEditionModel model)
        {
            var authorsModelsList = model.Authors;
            var authors = new List<Author>();

            foreach (var authorModel in authorsModelsList)
            {
                authors.Add(_authorRepository.FindById(authorModel.Id));
            }

            var entity = _mapper.Map<PrintingEdition>(model);

            entity.Authors = authors;

            _repository.Create(entity);

            //throw new NotImplementedException();
        }

        public PrintingEditionModel Get(PrintingEditionModel model)
        {
            throw new NotImplementedException();
        }

        public DataCollectionModel<PrintingEditionModel> GetAll(IndexRequestModel model)
        {
            throw new NotImplementedException();
        }

        public void Remove(PrintingEditionModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(PrintingEditionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
