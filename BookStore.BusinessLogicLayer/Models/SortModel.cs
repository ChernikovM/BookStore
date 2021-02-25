using BookStore.BusinessLogicLayer.Models.Base;

namespace BookStore.BusinessLogicLayer.Models
{
    public class SortModel : BaseModel
    {
        public string SortBy { get; set; } //Property name. $"{propName}+{asc/desc}"
    }
}
