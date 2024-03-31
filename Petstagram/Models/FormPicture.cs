using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Petstagram.Models
{
    public class FormPicture
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a name for your file.")]
        [MaxLength(30, ErrorMessage = "Name must be less than 30 characters long.")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "A picture is needed to add to database")]
        [ValidateNever]
        public IFormFile Picture { get; set; }
        [Required(ErrorMessage = "Please write a story for your picture.")]
        [MaxLength(1200, ErrorMessage = "Story must be less than 1200 characters long.")]
        public string Story {  get; set; }
        [ValidateNever]
        public DateTime UploadDate { get; set; }

        [Required(ErrorMessage = "At least one album needs to be selected.")]
        public List<int> PetIds { get; set; }

        public bool Replace { get; set; }
        [ValidateNever]
        public string Previous { get; set; }
    }
}
