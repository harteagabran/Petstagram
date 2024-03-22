using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Petstagram.Models
{
    public class FormLogIn
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(255)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(255)]
        public string Password { get; set; }
        [ValidateNever]
        public string? ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
