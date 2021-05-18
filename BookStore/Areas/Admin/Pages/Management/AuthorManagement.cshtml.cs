using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models.Admin;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Author;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Areas.Admin.Pages.Management
{
    [Authorize(Policy = "AdminCookiePolicy")]
    public class AuthorManagementModel : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {

        private readonly IAuthorService _authorService;

        public IndexRequestModel RequestModel { get; set; }

        public string Error { get; set; }

        public string Success { get; set; }

        public DataCollectionModel<AuthorModel> Collection { get; set; }

        #region Sort Filter Pagination
        public bool? SortDirect { get; set; } //false - desc, true - asc
        public string SortProperty { get; set; }

        public string FilterString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterProp { get; set; }
        public string FilterExpr { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterText { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        #endregion

        public AuthorManagementModel(IAuthorService authorService)
        {
            _authorService = authorService;

            RequestModel = new IndexRequestModel()
            {
                PageRequestModel = new PageRequestModel() { PageSize = PageSize },
            };
        }

        public async Task OnGetAsync(
            string sortProperty = "",
            bool? sortDirect = null,
            string filterString = "",
            string filterProp = "",
            string filterExpr = "",
            string filterText = "",
            int currentPage = 1,
            int pageSize = 10
            )
        {
            try
            {
                await FetchCollection(
                sortProperty,
                sortDirect,
                filterString,
                filterProp,
                filterExpr,
                filterText,
                currentPage,
                pageSize
                );
            }
            catch (CustomException ex)
            {
                Error = ex.ErrorMessages.First();
                //return;
            }
        }

        private async Task FetchCollection(
            string sortProperty = "",
            bool? sortDirect = null,
            string filterString = "",
            string filterProp = "",
            string filterExpr = "",
            string filterText = "",
            int currentPage = 1,
            int pageSize = 10
            )
        {
            var requestModel = new IndexRequestModel() { PageRequestModel = new PageRequestModel() { PageSize = pageSize, Page = currentPage } };

            if (!string.IsNullOrWhiteSpace(sortProperty))
            {
                var direct = sortDirect == true ? "asc" : "desc";
                requestModel.SortBy = $"{sortProperty}+{direct}";
            }

            if (!string.IsNullOrWhiteSpace(filterProp) && !string.IsNullOrWhiteSpace(filterExpr) && !string.IsNullOrWhiteSpace(filterText))
            {
                requestModel.Filter = $"{filterProp}.{filterExpr}(\"{filterText}\")";
            }

            Collection = await _authorService.GetAllAsync(requestModel);

            PageSize = Collection.PageModel.PageSize;
            CurrentPage = Collection.PageModel.CurrentPageNumber;
            var sort = Collection.Sort?.Split('+');
            SortProperty = sort?.First();
            if (sort?.Last() is not null)
            {
                SortDirect = sort?.Last()?.ToLower() == "asc";
            }
            FilterString = Collection.Filter;
            FilterProp = filterProp;
            FilterExpr = filterExpr;
            FilterText = filterText;
        }

        public PaginationModel GetPaginationModel()
        {
            var model = new PaginationModel
            {
                PageModel = Collection.PageModel,
                SortProperty = SortProperty,
                SortDirection = SortDirect,
                Filter = FilterString,
                ParentRoute = "AuthorManagement",
                FilterExpr = FilterExpr,
                FilterProp = FilterProp,
                FilterText = FilterText
            };

            return model;
        }

        public SortModel GetSortModel(string propName)
        {
            var model = new SortModel
            {
                PageModel = Collection.PageModel,
                SortProperty = SortProperty,
                SortDirection = SortDirect,
                Filter = FilterString,
                ParentRoute = "AuthorManagement",
                PropertyName = propName,
                FilterExpr = FilterExpr,
                FilterProp = FilterProp,
                FilterText = FilterText
            };

            return model;
        }

        public SelectList GetListProps()
        {
            var props = new List<string>
                {
                    "Name"
                };

            return new SelectList(props);
        }

        public SelectList GetListExpr()
        {
            var props = new List<string>
                {
                    "Contains",
                    "Equals"
                };

            return new SelectList(props);
        }

        public async Task OnGetDelete(
            long id,
            string sortProperty = "",
            string filterProp = "",
            string filterExpr = "",
            string filterText = "",
            string filterString = "",
            bool? sortDirect = null,
            int currentPage = 1,
            int pageSize = 10)
        {
            try
            {
                await _authorService.RemoveAsync(id);

                await FetchCollection(
                sortProperty,
                sortDirect,
                filterString,
                filterProp,
                filterExpr,
                filterText,
                currentPage,
                pageSize
                );

                Success = "Author was successfully deleted";
            }
            catch (CustomException ex)
            {
                Error = ex.ErrorMessages.First();
                //return;
            }
        }
    }
}
