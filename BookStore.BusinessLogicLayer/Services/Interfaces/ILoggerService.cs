using System;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface ILoggerService
    {
        public Task LogAsync(Exception ex);
    }
}
