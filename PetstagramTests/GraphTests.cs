using Petstagram.Models;
using Petstagram.StructureFactory;

namespace PetstagramTests
{
    public class GraphTests
    {
        [Fact]
        public void AddVertexWhenEmpty()
        {
            // Arrange
            Graph graph = new Graph();
            Picture picture = new();

            //Act
            graph.AddVertex(picture);

            //Assert
            Assert.Single(graph.Vertices);
        }

        [Fact]
        public void AddVertexWhenNotEmpty()
        {
            // Arrange
            Graph graph = new Graph();
            Picture picture = new();
            Picture picture2 = new();

            //Act
            graph.AddVertex(picture);
            graph.AddVertex(picture2);

            //Assert
            Assert.Equal(2, graph.Vertices.Count);
        }

        [Fact]
        public void AddEdgeWhenEmpty()
        {
            // Arrange
            Graph graph = new Graph();
            Picture picture = new();
            
            //Act
            graph.AddVertex(picture);

            //Assert
            Assert.Empty(graph.Edges);
        }

        [Fact]
        public void AddEdgeWhenNotEmpty()
        {
            // Arrange
            Graph graph = new Graph();
            Picture picture = new();
            Picture picture2 = new();

            //Act
            graph.AddVertex(picture);
            graph.AddVertex(picture2);


            //Assert
            Assert.Single(graph.Edges);
        }
    }
}