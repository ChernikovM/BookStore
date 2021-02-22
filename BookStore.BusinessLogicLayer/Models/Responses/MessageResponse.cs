using BookStore.BusinessLogicLayer.Models.Base;

namespace BookStore.BusinessLogicLayer.Models.Responses
{
    public class MessageResponse : BaseModel
    {
        public string Message { get; set; }
    }
}
