using BookStore.BusinessLogicLayer.Models.Base;
using System.Collections.Generic;

namespace BookStore.BusinessLogicLayer.Models.Responses
{
    public class IndexResponseModel<T> : BaseModel
    {
        public PageModel PageModel { get; set; }

        public string SortString { get; set; }

        public string FilterString { get; set; }

        public List<T> Data { get; set; } = new List<T>();
    }
}
