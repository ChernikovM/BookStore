using BookStore.BusinessLogicLayer.Models.Base;

namespace BookStore.BusinessLogicLayer.Models.Responses
{
    public class EmailConfirmationResponse : BaseModel
    {
        public string Response { get; set; }
    }
}
