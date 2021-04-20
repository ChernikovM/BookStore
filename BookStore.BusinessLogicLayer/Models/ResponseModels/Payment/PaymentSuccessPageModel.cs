using BookStore.BusinessLogicLayer.Models.Base;

namespace BookStore.BusinessLogicLayer.Models.ResponseModels.Payment
{
    public class PaymentSuccessPageModel: BaseModel
    {
        public long OrderId { get; set; }

    }
}
