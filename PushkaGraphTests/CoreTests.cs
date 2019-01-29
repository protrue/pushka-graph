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
            var graph = new Graph();
            graph.AddVertex();

            graph.Vertices.Length.Should().Be(1);
            graph.Edges.Length.Should().Be(0);
        }

        [TestMethod]
        public void AddVerticesShouldAddMultipleVertices()
        {
            var graph = new Graph();
            var addedVertices = graph.AddVertices(3);

            addedVertices.Count().Should().Be(3);
            graph.Vertices.Length.Should().Be(3);
            graph.Edges.Length.Should().Be(0);
        }

        [TestMethod]
        public void AddEdgeShouldAddEdge()
        {
            var graph = new Graph();
            graph.AddVertices(2);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[1]);

            graph.Vertices.Length.Should().Be(2);
            graph.Edges.Length.Should().Be(1);
        }
    }
}
