using BookStore.BusinessLogicLayer.Models.Base;

namespace BookStore.BusinessLogicLayer.Models.ResponseModels
{
    public class MessageResponse : BaseErrorModel
    {
        public string Message { get; set; }
    }
}
