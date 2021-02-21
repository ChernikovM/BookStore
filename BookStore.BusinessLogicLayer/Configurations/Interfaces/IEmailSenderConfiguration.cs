namespace BookStore.BusinessLogicLayer.Configurations.Interfaces
{
    public interface IEmailSenderConfiguration
    {
        public int Port { get; set; }
        public string Path { get; set; }
        public string FromName { get; set; }
        public string FromAddress { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}
