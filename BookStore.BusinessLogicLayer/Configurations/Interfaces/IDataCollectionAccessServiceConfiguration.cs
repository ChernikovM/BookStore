namespace BookStore.BusinessLogicLayer.Configurations.Interfaces
{
    public interface IDataCollectionAccessProviderConfiguration
    {
        public int DefaultPageSize { get; set; }

        public int DefaultPage { get; set; }

        public char SplitCharacter { get; set; }
    }
}
