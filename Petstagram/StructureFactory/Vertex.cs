using Petstagram.Models;

namespace Petstagram.StructureFactory
{
    public class Vertex
    {
        public int Id { get; set; }
        public Picture Pic { get; set; }
        public bool Visited { get; set; }

        public Vertex(Picture pic)
        {
            Pic = pic;
            Id = pic.Id;
            Visited = false;
        }
    }
}
