using BookStore.BusinessLogicLayer.Exceptions;
using System;
using System.Collections.Generic;

namespace BookStore.BusinessLogicLayer.Models.Base
{
    public class BaseErrorModel
    {
        public List<string> Errors { get; set; }

        public BaseErrorModel()
        {
            Errors = new List<string>();
        }

        public BaseErrorModel(Exception ex) : this()
        {
            if (ex is CustomException)
            {
                Errors.AddRange((ex as CustomException).ErrorMessages);
                return;
            }
            Errors.Add(ex.Message);
        }
    }
}
