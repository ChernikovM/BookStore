using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models.Admin;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels.User;
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
    public class UserManagementModel : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        private readonly IUserService _userService;

        public IndexRequestModel RequestModel { get; set; }

        public string Error { get; set; }

        public string Success { get; set; }

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

        public DataCollectionModel<UserResponseModel> Collection { get; set; }

        public UserManagementModel(IUserService userService)
        {
            _userService = userService;

            RequestModel = new IndexRequestModel() { 
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

        public async Task OnGetBanUser(
            string id,
            string sortProperty = "",
            bool? sortDirect = null,
            string filterString = "",
            string filterProp = "",
            string filterExpr = "",
            string filterText = "",
            int currentPage = 1,
            int pageSize = 10)
        {
            try
            {
                var response = await _userService.BlockUser(id, null);
                Success = response.Message;

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

        public async Task OnGetUnbanUser(
            string id,
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
                var response = await _userService.UnblockUser(id);
                Success = response.Message;

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

            Collection = await _userService.GetAllUsers(requestModel);

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
                ParentRoute = "UserManagement",
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
                ParentRoute = "UserManagement",
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
                    "FirstName",
                    "LastName",
                    "Email"
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

    }
}
