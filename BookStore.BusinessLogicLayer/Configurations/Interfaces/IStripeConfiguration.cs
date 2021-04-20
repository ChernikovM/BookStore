namespace BookStore.BusinessLogicLayer.Configurations.Interfaces
{
    public interface IStripeConfiguration
    {
        string PublicKey { get; set; }

        string PrivateKey { get; set; }
    }
}
