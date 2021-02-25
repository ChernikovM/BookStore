using BookStore.BusinessLogicLayer.Models.Base;
using System;

namespace BookStore.BusinessLogicLayer.Models
{
    public class PageModel : BaseModel
    {
        public int CurrentPageNumber { get; set; }

        public int TotalPages { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (CurrentPageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (CurrentPageNumber < TotalPages);
            }
        }


        public PageModel(int count, int currentPageNumber, int pageSize)
        {
            CurrentPageNumber = currentPageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        
    }
}
