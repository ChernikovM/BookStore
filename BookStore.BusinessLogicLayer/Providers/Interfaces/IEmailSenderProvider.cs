using BookStore.DataAccessLayer.Entities;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Providers.Interfaces
{
    public interface IEmailSenderProvider
    {
        public Task SendEmailConfirmationLinkAsync(User user, string callbackUrl);

        public Task SendPasswordResettingLinkAsync(User user, string callbackUrl);
    }
}
