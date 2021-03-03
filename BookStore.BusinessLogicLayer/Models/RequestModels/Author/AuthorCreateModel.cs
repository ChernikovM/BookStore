using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.RequestModels.Author
{
    public class AuthorCreateModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
