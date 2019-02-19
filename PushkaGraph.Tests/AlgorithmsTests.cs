using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PushkaGraph.Algorithms;
using PushkaGraph.Core;

namespace PushkaGraph.Tests
{
    [TestClass]
    public class AlgorithmsTests
    {
        [TestMethod]
        public void ExampleExtensionMethodShouldDoNothing()
        {
            var graph = new Graph();

            graph.SomeAlgorithm(null).Should().BeNull();
        }
        [TestMethod]
        public void ShortestPath_smallgraph()
        {
            var graph = new Graph(3);

            graph.AddEdge(graph.Vertices[0], graph.Vertices[1]);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[2]);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[2]);

            graph.ShortestPath(graph.Vertices[2], graph.Vertices[1]).Should().BeNullOrEmpty();
        }
        [TestMethod]
        public void ShortestPath_largegraph()
        {
            var graph = new Graph(6);

            graph.AddEdge(graph.Vertices[0], graph.Vertices[1], 7);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[2], 3);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[3], 6);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[4], 2);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[5], 3);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[2], 4);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[4], 6);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[5], 6);
            graph.AddEdge(graph.Vertices[2], graph.Vertices[3], 1);
            graph.AddEdge(graph.Vertices[2], graph.Vertices[4], 4);
            graph.AddEdge(graph.Vertices[3], graph.Vertices[4], 5);
            graph.AddEdge(graph.Vertices[4], graph.Vertices[5], 4);

            graph.ShortestPath(graph.Vertices[2], graph.Vertices[1]).Should().BeNullOrEmpty();
        }
        [TestMethod]
        public void ShortestPath_fullgraph()
        {
            var graph = new Graph(10);

            graph.AddEdge(graph.Vertices[0], graph.Vertices[1], 7);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[2], 3);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[3], 6);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[4], 2);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[5], 3);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[6], 3);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[7], 3);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[8], 3);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[9], 3);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[2], 4);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[3], 4);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[4], 6);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[5], 6);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[6], 4);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[7], 4);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[8], 4);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[9], 4);
            graph.AddEdge(graph.Vertices[2], graph.Vertices[3], 1);
            graph.AddEdge(graph.Vertices[2], graph.Vertices[4], 4);
            graph.AddEdge(graph.Vertices[2], graph.Vertices[5], 4);
            graph.AddEdge(graph.Vertices[2], graph.Vertices[6], 4);
            graph.AddEdge(graph.Vertices[2], graph.Vertices[7], 4);
            graph.AddEdge(graph.Vertices[2], graph.Vertices[8], 4);
            graph.AddEdge(graph.Vertices[2], graph.Vertices[9], 9);
            graph.AddEdge(graph.Vertices[3], graph.Vertices[4], 5);
            graph.AddEdge(graph.Vertices[3], graph.Vertices[5], 5);
            graph.AddEdge(graph.Vertices[3], graph.Vertices[6], 5);
            graph.AddEdge(graph.Vertices[3], graph.Vertices[7], 5);
            graph.AddEdge(graph.Vertices[3], graph.Vertices[8], 5);
            graph.AddEdge(graph.Vertices[3], graph.Vertices[9], 5);
            graph.AddEdge(graph.Vertices[4], graph.Vertices[5], 4);
            graph.AddEdge(graph.Vertices[4], graph.Vertices[6], 4);
            graph.AddEdge(graph.Vertices[4], graph.Vertices[7], 4);
            graph.AddEdge(graph.Vertices[4], graph.Vertices[8], 4);
            graph.AddEdge(graph.Vertices[4], graph.Vertices[9], 4);
            graph.AddEdge(graph.Vertices[5], graph.Vertices[6], 4);
            graph.AddEdge(graph.Vertices[5], graph.Vertices[7], 4);
            graph.AddEdge(graph.Vertices[5], graph.Vertices[8], 4);
            graph.AddEdge(graph.Vertices[5], graph.Vertices[9], 4);
            graph.AddEdge(graph.Vertices[6], graph.Vertices[7], 4);
            graph.AddEdge(graph.Vertices[6], graph.Vertices[8], 4);
            graph.AddEdge(graph.Vertices[6], graph.Vertices[9], 4);
            graph.AddEdge(graph.Vertices[7], graph.Vertices[8], 4);
            graph.AddEdge(graph.Vertices[7], graph.Vertices[9], 4);
            graph.AddEdge(graph.Vertices[8], graph.Vertices[9], 4);

            graph.ShortestPath(graph.Vertices[2], graph.Vertices[9]).Should().BeNullOrEmpty();
        }
        [TestMethod]
        public void ShortestPath_Emptygraph()
        {
            var graph = new Graph(10);
            
            graph.ShortestPath(graph.Vertices[2], graph.Vertices[9]).Should().BeNullOrEmpty();
        }
    }
}
