﻿using System.ComponentModel.DataAnnotations;

namespace WebShopWebApp.ViewModels
{
    public class UserEditProfileVM
    {
        [Required(ErrorMessage = "User ID is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "User name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "Password should be at least 8 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name should be between 2 and 50 characters long")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name should be between 2 and 50 characters long")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Provide a correct e-mail address")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Provide a correct phone number")]
        public string Phone { get; set; }
    }
}
