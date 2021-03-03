using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.RequestModels
{
    public class PageRequestModel
    {
        public int? Page { get; set; }

        [Required]
        [Range(1, 100)]
        public int? PageSize { get; set; }
    }
}
