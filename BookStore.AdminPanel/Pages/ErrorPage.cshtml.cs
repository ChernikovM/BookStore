using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BookStore.AdminPanel.Pages
{
    public class ErrorPageModel : PageModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public int ErrorStatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet(int code)
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            ErrorStatusCode = code;

            

            var builder = new StringBuilder();
            //if(errors is not null && errors.Count > 0)
            //{
            //    foreach(var error in errors)
            //    {
            //        builder.Append(error);
            //    }
            //}

            ErrorMessage = ErrorStatusCode switch
            {
                400 => builder.ToString(),
                404 => "The requested page not found.",
                500 => "My custom 500 error message.",
                _ => "An error occurred while processing your request.",
            };
        }
    }
}
