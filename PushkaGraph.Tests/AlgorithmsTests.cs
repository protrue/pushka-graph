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
        public void ConnectedComponents_EmptyGraph()
        {
            var graph = new Graph();
            var connComponentsCount = graph.ConnectedComponentsCount();

            Assert.AreEqual(0, connComponentsCount);
        }

        [TestMethod]
        public void ConnectedComponents_OneVertex()
        {
            var graph = new Graph(1);
            var connComponentsCount = graph.ConnectedComponentsCount();

            Assert.AreEqual(1, connComponentsCount);
        }

        [TestMethod]
        public void ConnectedComponents_ThreeNotСonnectedVertices()
        {
            var graph = new Graph(3);

            var connComponentsCount = graph.ConnectedComponentsCount();
            Assert.AreEqual(3, connComponentsCount);
        }

        [TestMethod]
        public void ConnectedComponents_ThreeVerticesOneComponent()
        {
            var graph = new Graph(3);

            graph.AddEdge(graph.Vertices[0], graph.Vertices[1]);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[2]);

            var connComponentsCount = graph.ConnectedComponentsCount();
            Assert.AreEqual(1, connComponentsCount);
        }

        [TestMethod]
        public void ConnectedComponents_ThreeСomponents()
        {
            var graph = new Graph(6);

            graph.AddEdge(graph.Vertices[0], graph.Vertices[1]);
            graph.AddEdge(graph.Vertices[2], graph.Vertices[3]);
            graph.AddEdge(graph.Vertices[2], graph.Vertices[4]);
            graph.AddEdge(graph.Vertices[3], graph.Vertices[4]);

            var connComponentsCount = graph.ConnectedComponentsCount();
            Assert.AreEqual(3, connComponentsCount);
        }
    }
}
