using BookStore.BusinessLogicLayer.Configurations.Interfaces;

namespace BookStore.BusinessLogicLayer.Configurations
{
    public class StripeConfiguration : IStripeConfiguration
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}
