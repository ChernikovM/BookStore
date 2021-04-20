using BookStore.DataAccessLayer.AppContext;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Base;
using BookStore.DataAccessLayer.Repositories.EFRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataAccessLayer.Repositories.EFRepositories
{
    public class PrintingEditionRepository : EFRepository<PrintingEdition>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(DataContext context) : base(context)
        { 
            
        }

        public override async Task<PrintingEdition> FindByIdAsync(long id)
        {
            var result = await _dbSet
                .Include(x => x.Authors)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsRemoved == false);

            return result;
        }

        public override async Task<IQueryable<PrintingEdition>> GetAllAsync()
        {
            var result = _dbSet
                .Include(x => x.Authors)
                .Where(x => x.IsRemoved == false);

            return result;
        }

        public override async Task<List<PrintingEdition>> FindByIdAsync(List<long> ids)
        {
            var result = await _dbSet
                .Include(x => x.Authors)
                .Where(x => ids.Contains(x.Id) && x.IsRemoved == false)
                .ToListAsync();

            return result;
        }
    }
}
