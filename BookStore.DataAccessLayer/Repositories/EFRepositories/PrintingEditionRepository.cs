using BookStore.DataAccessLayer.AppContext;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Base;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;

namespace BookStore.DataAccessLayer.Repositories.EFRepositories
{
    public class PrintingEditionRepository : EFRepository<PrintingEdition>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(DataContext context) : base(context)
        { 
            
        }
    }
}
