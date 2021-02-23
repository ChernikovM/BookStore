using BookStore.BusinessLogicLayer.Configurations.Interfaces;

namespace BookStore.BusinessLogicLayer.Configurations
{
    public class EmailSenderConfiguration : IEmailSenderConfiguration
    {
        public int Port { get; set; }
        public string PathEmailConfirmation { get; set; }
        public string PathPasswordChange { get; set; }
        public string FromName { get; set; }
        public string FromAddress { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}
