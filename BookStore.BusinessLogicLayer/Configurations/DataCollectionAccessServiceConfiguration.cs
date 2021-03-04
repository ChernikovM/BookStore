using BookStore.BusinessLogicLayer.Configurations.Interfaces;

namespace BookStore.BusinessLogicLayer.Configurations
{
    public class DataCollectionAccessServiceConfiguration : IDataCollectionAccessProviderConfiguration
    {
        public int DefaultPageSize { get; set; }

        public int DefaultPage { get; set; }

        public char SplitCharacter { get; set; }
    }
}
