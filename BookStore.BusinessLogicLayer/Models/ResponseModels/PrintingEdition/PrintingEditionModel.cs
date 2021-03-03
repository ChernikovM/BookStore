﻿using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.DataAccessLayer.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.ResponseModel.PrintingEdition
{
    public class PrintingEditionModel : BaseModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Invalid price.")]
        public decimal Price { get; set; }
        
        [Required]
        [EnumDataType(typeof(Enums.CurrencyType))]
        public int Currency { get; set; }

        [Required]
        [EnumDataType(typeof(Enums.PrintingEditionType))]
        public int Type { get; set; }

        [Required]
        public string Description { get; set; }

        public List<BaseModel> Authors { get; set; }
    }
}