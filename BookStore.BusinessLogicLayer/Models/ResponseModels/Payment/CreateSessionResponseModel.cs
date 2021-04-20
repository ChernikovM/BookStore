using BookStore.BusinessLogicLayer.Models.Base;

namespace BookStore.BusinessLogicLayer.Models.ResponseModels.Payment
{
    public class CreateSessionResponseModel : BaseModel
    {
        public string SessionId { get; set; }
    }
}
