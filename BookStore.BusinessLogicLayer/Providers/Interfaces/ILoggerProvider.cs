using System;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Providers.Interfaces
{
    public interface ILoggerProvider
    {
        public Task LogAsync(Exception ex);
    }
}
