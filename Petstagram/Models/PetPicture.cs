namespace Petstagram.Models
{
    public class PetPicture
    {
        public int PetId { get; set; }
        public Pet Pet { get; set; }

        public int PictureId { get; set; }
        public Picture Picture { get; set; }
    }
}