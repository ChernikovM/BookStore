using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BookStore.BusinessLogicLayer.Services
{
    public class ValidationService : IValidationService
    {
        public bool ValidateModel<T>(T model)
        {
            var errors = new List<ValidationResult>();
            var context = new ValidationContext(model);

            if (!Validator.TryValidateObject(model, context, errors, true))
            {
                throw new CustomException(HttpStatusCode.BadRequest, errors);
            }

            return true;
        }

        public bool ValidateProperty(object obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
