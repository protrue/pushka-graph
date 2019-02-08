using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PushkaGraph.Core;

namespace PushkaGraph.NewAlgorithms
{
    public class GraphAlgorithmResult
    {
        public int? Number { get; }

        public Vertex[] Vertices { get; }

        public Edge[] Edges { get; }

        public string StringResult { get; }

        public GraphAlgorithmResult(int? number = null, Vertex[] vertices = null, Edge[] edges = null, string stringResult = null)
        {
            Number = number;
            Vertices = vertices;
            Edges = edges;
            StringResult = stringResult;
        }
    }
}
