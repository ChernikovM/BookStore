namespace BookStore.BusinessLogicLayer.Models
{
    public class IndexRequestModel
    {
        public string SortBy { get; set; }

        public PageRequestModel PageRequestModel { get; set; }

        public string Filter { get; set; }
    }
}
