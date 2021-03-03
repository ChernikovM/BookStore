using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.RequestModels
{
    public class IndexRequestModel
    {
        public string SortBy { get; set; }

        [Required]
        public PageRequestModel PageRequestModel { get; set; }

        public string Filter { get; set; }
    }
}
