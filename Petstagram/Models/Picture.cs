using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petstagram.Models
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string FileName { get; set; }
        public string Story {  get; set; }

        [DataType(DataType.Date)]
        public DateTime UploadDate { get; set; }

        //Navigation for pictures that have multiple pets
        public List<Pet> Pets { get; set; }
    }
}
