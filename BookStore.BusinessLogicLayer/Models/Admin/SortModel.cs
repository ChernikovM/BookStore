using BookStore.BusinessLogicLayer.Models.ResponseModels;

namespace BookStore.BusinessLogicLayer.Models.Admin
{
    public class SortModel
    {
        public PageModel PageModel { get; set; }

        public string ParentRoute { get; set; }

        public string SortProperty { get; set; }

        public bool? SortDirection { get; set; }

        public string PropertyName { get; set; }

        public bool? Direction { get; set; }

        public string Filter { get; set; }

        public string FilterProp { get; set; }

        public string FilterExpr { get; set; }

        public string FilterText { get; set; }
    }
}
