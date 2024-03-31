namespace Petstagram.StructureFactory
{
    public class Edge
    {
        public int Start { get; set; }
        public int End { get; set; }

        public Edge(int start, int end)
        {
            Start = start;
            End = end;
        }
    }
}
