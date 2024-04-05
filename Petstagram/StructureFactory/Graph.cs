using Petstagram.Models;

namespace Petstagram.StructureFactory
{
    public class Graph
    {
        public List<Vertex> Vertices { get; set; }
        public List<Edge> Edges { get; set; }

        public Graph()
        {
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();
        }

        //Connect to one of the vertices to maintain a complete graph
        public void AddVertex(Picture pic)
        {
            Vertex add = new Vertex(pic);
            
            if(!IsEmpty())
            {
                Vertex connect = GetRandomVertex();
                AddEdge(add.Id, connect.Id);
            }
            
            Vertices.Add(add);
        }
        public bool IsEmpty()
        {
            return Vertices.Count == 0;
        }
        public Vertex GetVertex(int id)
        {
            return Vertices.Find(v => v.Id == id);
        }

        public Vertex GetRandomVertex()
        {
            Random random = new Random();
            int index = random.Next(0, Vertices.Count);
            return Vertices[index];
        }

        public Picture GetRandomPic()
        {
            Vertex v = GetRandomVertex();
            return v.Pic;
        }

        public void AddEdge(int start, int end)
        {
            Edges.Add(new Edge(start, end));
        }

        public List<Edge> GetEdgesOfVertex(Vertex vertex)
        {
            return Edges.FindAll(e => e.Start == vertex.Id || e.End == vertex.Id);
        }

        public void SetVisited(Vertex vertex)
        {
            vertex.Visited = true;
        }

        public string GetVisited()
        {
            int total = Vertices.Count;
            int visited = Vertices.FindAll(v => v.Visited == true).Count;
            return $"{visited}/{total}";
        }
    }
}
