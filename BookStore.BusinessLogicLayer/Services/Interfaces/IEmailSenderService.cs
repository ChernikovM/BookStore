using BookStore.DataAccessLayer.Entities;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IEmailSenderService
    {
        public Task SendEmailConfirmationLinkAsync(User user);
    }
}
