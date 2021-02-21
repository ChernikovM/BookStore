using BookStore.BusinessLogicLayer.Exceptions;
using System;
using System.Collections.Generic;

namespace BookStore.BusinessLogicLayer.Models.Base
{
    public class BaseModel
    {
        public List<string> Errors { get; set; } = new List<string>();

        public BaseModel()
        { 
        
        }

        public BaseModel(Exception ex)
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
