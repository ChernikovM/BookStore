using BookStore.BusinessLogicLayer.Models.Base;
using System.Collections.Generic;

namespace BookStore.BusinessLogicLayer.Models.Responses
{
    public class DataCollectionModel<T> : BaseModel
    {
        public PageModel PageModel { get; set; }

        public string Sort { get; set; }

        public string Filter { get; set; }

        public List<T> DataCollection { get; set; } = new List<T>();
    }
}
