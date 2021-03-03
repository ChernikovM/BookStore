using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace BookStore.BusinessLogicLayer.Exceptions
{
    public class CustomException : Exception
    {
        public List<string> ErrorMessages { get; set; } = new List<string>();
        
        public HttpStatusCode Code { get; set; }

        
        private CustomException(HttpStatusCode code)
        {
            Code = code;
        }

        public CustomException(HttpStatusCode code, IEnumerable<string> errorMessages) : this(code)
        {
            ErrorMessages.AddRange(errorMessages);
        }

        public CustomException(HttpStatusCode code, List<ValidationResult> errorMessages) : this(code)
        {
            ErrorMessages.AddRange(errorMessages.Select(x => x.ErrorMessage));
        }

        public CustomException(HttpStatusCode code, string errorMessage) : this(code)
        {
            ErrorMessages.Add(errorMessage);
        }

        public CustomException(HttpStatusCode code, IdentityResult errors) : this(code)
        {
            foreach (var error in errors.Errors)
            {
                ErrorMessages.Add(error.Description);
            }
        }
    }
}
