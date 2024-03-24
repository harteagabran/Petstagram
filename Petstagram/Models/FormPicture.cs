using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Petstagram.Models
{
    public class FormPicture
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a name for your file.")]
        [MaxLength(50)]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "A picure is needed to add to database")]
        public IFormFile Picture { get; set; }
        public string? Story {  get; set; }
        [ValidateNever]
        public DateTime UploadDate { get; set; }

        [Required(ErrorMessage = "At least one album needs to be selected.")]
        public List<int> PetIds { get; set; }
    }
}
