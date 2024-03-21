using System.ComponentModel.DataAnnotations;

namespace Petstagram.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string OwnerId { get; set; }

        public ICollection<Picture> Pictures { get; set; }
    }
}
