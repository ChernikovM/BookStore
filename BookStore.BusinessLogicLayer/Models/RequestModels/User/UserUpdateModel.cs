﻿using BookStore.BusinessLogicLayer.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.RequestModels.User
{
    public class UserUpdateModel : BaseErrorModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
