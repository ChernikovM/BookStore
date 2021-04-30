using BookStore.AdminPanel.Models.Base;

namespace BookStore.AdminPanel.Models
{
    public class PageResponseModel: BaseModel
    {
        public int CurrentPageNumber { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextpage { get; set; }
    }
}
