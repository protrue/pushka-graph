using FluentAssertions;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PushkaGraph.Core;
using PushkaGraph.Algorithms;
using System.Linq;
namespace PushkaGraph.Tests
{
    [TestClass]
    public class AlgorithmsTests
    {
        [TestMethod]
        public void MST_EmptyGraph()
        {
            var graph = new Graph();
            var mst = graph.MST();

            Assert.AreEqual(null, mst);
        }
        [TestMethod]
        public void MstOneVertex()
        {
            var graph = new Graph(1);
            var mst = graph.MST();

            Assert.AreEqual(null, mst);
        }
        [TestMethod]
        public void MstTwoVertex()
        {
            var graph = new Graph(2);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[1]);
            var mst = graph.MST();

            Assert.AreEqual(1, mst);
        }
        private void checkMST(Graph graph, List<Edge> path)
        {
            var selectedEdges = new List<Edge>(path);
            var graphVertices = new HashSet<Vertex>(graph.Vertices);
            var tmp = new List<Vertex>() ;
            for (int i = 0; i < selectedEdges.Count; i++)
            { tmp.Add(selectedEdges[i].FirstVertex); tmp.Add(selectedEdges[i].SecondVertex); }
            var tmp2 = new HashSet<Vertex>(tmp);
            Assert.IsTrue(graphVertices.SetEquals(tmp2),
                "MST should contain all the graph vertices");
        }

        [TestMethod]
        public void Mst4Vertices()
        {
            var graph = new Graph(4);

            graph.AddEdge(graph.Vertices[0], graph.Vertices[1], 2);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[2]);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[2], 4);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[3], 5);

            checkMST(graph, new List<Edge>(graph.MST()));
        }
        [TestMethod]
        public void Mst5Vertices()
        {
            var graph = new Graph(5);

            graph.AddEdge(graph.Vertices[0], graph.Vertices[1], 2);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[2], 4);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[2], 3);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[3], 2);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[4], 1);

            checkMST(graph, new List<Edge>(graph.MST()));
        }
        [TestMethod]
        public void Mst3Full()
        {
            var graph = new Graph(3);

            graph.AddEdge(graph.Vertices[0], graph.Vertices[1], 2);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[2], 3);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[3]);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[2], 6);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[3]);
            graph.AddEdge(graph.Vertices[2], graph.Vertices[3], 8);

            checkMST(graph, new List<Edge>(graph.MST()));
        }
    }
}
