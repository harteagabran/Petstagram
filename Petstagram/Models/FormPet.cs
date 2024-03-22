using System.ComponentModel.DataAnnotations;

namespace Petstagram.Models
{
    public class FormPet
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "a Name of the pet album must be given.")]
        public string Name { get; set; }
        public string OwnerId { get; set; }
    }
}
