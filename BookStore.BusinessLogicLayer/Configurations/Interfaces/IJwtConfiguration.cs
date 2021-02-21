namespace BookStore.BusinessLogicLayer.Configurations.Interfaces
{
    public interface IJwtConfiguration
    {
        string Secret { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        int AccessTokenExpiration { get; set; }
        int RefreshTokenExpiration { get; set; }
    }
}
