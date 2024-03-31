using Petstagram.Models;

namespace Petstagram.StructureFactory
{
    public class GraphFactory
    {
        public Graph CreateGraph(List<Picture> pictures)
        {
            Graph graph = new Graph();

            //add vertices
            foreach (Picture pic in pictures)
            {
                graph.AddVertex(pic);
            }

            return graph;
        }
    }
}
