using BookStore.BusinessLogicLayer.Configurations.Interfaces;

namespace BookStore.BusinessLogicLayer.Configurations
{
    public class LoggerConfiguration : ILoggerConfiguration
    {
        public string FilePath { get; set; }

    }
}
