using System.Collections.Generic;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public abstract class ISortService
    {
        public abstract List<T> SortBy<T>(string sortRules);
    }
}
