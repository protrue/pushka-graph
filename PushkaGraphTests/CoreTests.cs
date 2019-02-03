using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PushkaGraphCore;

namespace PushkaGraphTests
{
    [TestClass]
    public class CoreTests
    {
        [TestMethod]
        public void AddVertexShouldAddOneVertex()
        {
            var graph = new Graph(1);

            graph.Vertices.Length.Should().Be(1);
            graph.Edges.Length.Should().Be(0);

            graph.Vertices[0].AdjacentVertices.Length.Should().Be(0);
            graph.Vertices[0].IncidentEdges.Length.Should().Be(0);
        }

        [TestMethod]
        public void AddVerticesShouldAddMultipleVertices()
        {
            var graph = new Graph(3);

            graph.Vertices.Length.Should().Be(3);
            graph.Edges.Length.Should().Be(0);

            foreach (var graphVertex in graph.Vertices)
            {
                graphVertex.AdjacentVertices.Length.Should().Be(0);
                graphVertex.IncidentEdges.Length.Should().Be(0);
            }
        }

        [TestMethod]
        public void AddEdgeShouldAddOneEdge()
        {
            var graph = new Graph(2);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[1]);

            graph.Vertices.Length.Should().Be(2);
            graph.Edges.Length.Should().Be(1);

            graph.Vertices[0].AdjacentVertices[0].Should().Be(graph.Vertices[1]);
            graph.Vertices[1].AdjacentVertices[0].Should().Be(graph.Vertices[0]);

            graph.Edges[0].FirstVertex.Should().Be(graph.Vertices[0]);
            graph.Edges[0].SecondVertex.Should().Be(graph.Vertices[1]);
        }

        [TestMethod]
        public void DeleteEdgeShouldDeleteEdge()
        {
            var graph = new Graph(2);
            var edge = graph.AddEdge(graph.Vertices[0], graph.Vertices[1]);
            graph.DeleteEdge(edge);

            graph.Vertices.Length.Should().Be(2);
            graph.Edges.Length.Should().Be(0);

            graph.Vertices[0].AdjacentVertices.Length.Should().Be(0);
            graph.Vertices[0].IncidentEdges.Length.Should().Be(0);

            graph.Vertices[1].AdjacentVertices.Length.Should().Be(0);
            graph.Vertices[1].IncidentEdges.Length.Should().Be(0);
        }
    }
}
