using System;
using PushkaGraph.Core;

namespace PushkaGraph.NewAlgorithms.Wrapper
{
    public class GraphAlgorithmParameters
    {
        public Graph Graph { get; }

        public int RequiredVerticesCount { get; }

        public int RequiredEdgesCount { get; }

        public Vertex[] Vertices { get; }

        public Edge[] Edges { get; }

        public GraphAlgorithmParameters(Graph graph = null, Vertex[] vertices = null, int requiredVerticesCount = 0, Edge[] edges = null, int requiredEdgesCount = 0)
        {
            Graph = graph;
            Vertices = vertices;
            RequiredVerticesCount = requiredVerticesCount;
            Edges = edges;
            RequiredEdgesCount = requiredEdgesCount;
        }
    }
}
