using BookStore.AdminPanel.Models.Base;
using System.Collections.Generic;

namespace BookStore.AdminPanel.Models
{
    public class CollectionResponseModel<T>: BaseModel
    {
        public PageResponseModel PageModel { get; set; }

        public string Sort { get; set; }

        public string Filter { get; set; }

        public List<T> DataCollection { get; set; } = new List<T>();

        public int CollectionCount { get; set; }
    }
}
