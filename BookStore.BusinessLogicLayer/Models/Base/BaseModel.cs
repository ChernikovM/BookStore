using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.Base
{
    public class BaseModel : BaseErrorModel
    {
        [Required(AllowEmptyStrings = false)]
        [Range(1, long.MaxValue, ErrorMessage = "Invalid id.")]
        public long? Id { get; set; }
    }
}
