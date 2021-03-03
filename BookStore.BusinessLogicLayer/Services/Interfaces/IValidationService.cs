namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IValidationService
    {
        bool ValidateModel<T>(T model);
        bool ValidateProperty(object obj);
    }
}
