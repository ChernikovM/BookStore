namespace BookStore.AdminPanel.Models
{
    public class CollectionRequestModel
    {
        public string SortBy { get; set; }

        public PageRequestModel PageRequestModel { get; set; }

        public string Filter { get; set; }
    }
}
