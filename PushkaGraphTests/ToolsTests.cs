using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PushkaGraphCore;
using PushkaGraphTools;

namespace PushkaGraphTests
{
    [TestClass]
    public class ToolsTests
    {
        [TestMethod]
        public void AdjacencyMatrixOfEmptyGraphShouldBeEmpty()
        {
            var graph = new Graph();
            var adjacencyMatrix = graph.GetAdjacencyMatrix();

            adjacencyMatrix.GetLength(0).Should().Be(0);
            adjacencyMatrix.GetLength(1).Should().Be(0);
        }

        [TestMethod]
        public void AdjacencyMatrixOfGraphWithOneVertex()
        {
            var graph = new Graph(1);
            var adjacencyMatrix = graph.GetAdjacencyMatrix();

            adjacencyMatrix.GetLength(0).Should().Be(1);
            adjacencyMatrix.GetLength(1).Should().Be(1);
            adjacencyMatrix[0, 0].Should().Be(0);
        }


        [TestMethod]
        public void AdjacencyMatrixOfGraphWithThreeVertex()
        {
            var graph = new Graph(3);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[1], 2);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[2], 3);

            var adjacencyMatrix = graph.GetAdjacencyMatrix();

            adjacencyMatrix.GetLength(0).Should().Be(3);
            adjacencyMatrix.GetLength(1).Should().Be(3);
            adjacencyMatrix.Should().BeEquivalentTo(
                new int[3, 3]
                {
                    { 0, 2, 0 },
                    { 2, 0, 3 },
                    { 0, 3, 0 }
                });
        }
    }
}
