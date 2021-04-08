using BookStore.DataAccessLayer.Entities;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Providers.Interfaces
{
    public interface IEmailSenderProvider
    {
        public Task SendEmailConfirmationLinkAsync(User user);

        public Task SendPasswordResettingLinkAsync(User user, string newPassword);
    }
}
