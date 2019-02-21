using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PushkaGraph.Core;
using PushkaGraph.Tools;

namespace PushkaGraph.Tests
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

        [TestMethod]
        public void CleanEdgesShouldDeleteAllEdges()
        {
            var graph = new Graph(3);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[1], 2);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[2], 3);

            graph.CleanEdges();

            graph.Edges.Length.Should().Be(0);
        }

        [TestMethod]
        public void CleanVerticesShouldDeleteAllVerticesAndEdges()
        {
            var graph = new Graph(3);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[1], 2);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[2], 3);

            graph.CleanVertices();

            graph.Vertices.Length.Should().Be(0);
            graph.Edges.Length.Should().Be(0);
        }

        [TestMethod]
        public void CreateGraphFromAdjacencyMatrixTest()
        {
            var graph = new Graph();

            Action createFromBadMatrix = () => graph.CreateFromAdjacencyMatrix(new[,]
            {
                { 0, 1 },
                { 0, 0 }
            });

            createFromBadMatrix.Should().Throw<ArgumentException>();

            graph.CreateFromAdjacencyMatrix(new[,]
            {
                { 0, 1 },
                { 1, 0 }
            });

            graph.Vertices.Length.Should().Be(2);
            graph.Edges.Length.Should().Be(1);

            graph.Edges[0].FirstVertex.Should().Be(graph.Vertices[0]);
            graph.Edges[0].SecondVertex.Should().Be(graph.Vertices[1]);
        }
    }
}
