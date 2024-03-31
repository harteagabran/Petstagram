using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Petstagram.Models
{
    public class FormPet
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "a Name of the pet album must be given.")]
        [MaxLength(30, ErrorMessage = "a Name of the pet album must be less than 30 characters long.")]
        public string Name { get; set; }
        public string OwnerId { get; set; }
        [ValidateNever]
        public string Previous {  get; set; }
    }
}
