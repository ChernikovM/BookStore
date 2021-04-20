using BookStore.BusinessLogicLayer.Models.Base;

namespace BookStore.BusinessLogicLayer.Models.ResponseModels.Payment
{
    public class PaymentModel : BaseModel
    {
        public string SessionId { get; set; }
    }
}
