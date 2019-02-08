using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PushkaGraph.Core;

namespace PushkaGraph.NewAlgorithms
{
    public class GraphAlgorithmParameters
    {
        public Graph Graph { get; }

        public Vertex[] Vertices { get; }

        public Edge[] Edges { get; }

        public GraphAlgorithmParameters(Graph graph = null, Vertex[] vertices = null, Edge[] edges = null)
        {
            Graph = graph;
            Vertices = vertices;
            Edges = edges;
        }
    }
}
