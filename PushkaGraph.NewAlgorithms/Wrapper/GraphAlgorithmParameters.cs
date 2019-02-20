using System;
using PushkaGraph.Core;

namespace PushkaGraph.NewAlgorithms.Wrapper
{
    public class GraphAlgorithmParameters
    {
        public Graph Graph { get; }

        public Tuple<Vertex,Vertex> VerticesPair { get; set; }

        public Vertex[] Vertices { get; }

        public Edge[] Edges { get; }

        public GraphAlgorithmParameters(Graph graph = null, Tuple<Vertex, Vertex> verticesPair = null, Vertex[] vertices = null, Edge[] edges = null)
        {
            Graph = graph;
            VerticesPair = verticesPair;
            Vertices = vertices;
            Edges = edges;
        }
    }
}
