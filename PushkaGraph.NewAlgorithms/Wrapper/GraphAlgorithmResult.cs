using PushkaGraph.Core;

namespace PushkaGraph.NewAlgorithms.Wrapper
{
    public class GraphAlgorithmResult
    {
        public int? Number { get; }

        public  Vertex[] Vertices { get; }

        public Edge[] Edges { get; }

        public bool IsSequential { get; set; }

        public string StringResult { get; }

        public GraphAlgorithmResult(int? number = null, Vertex[] vertices = null,
            Edge[] edges = null, string stringResult = null)
        {
            Number = number;
            Vertices = vertices;
            Edges = edges;
            StringResult = stringResult;
        }
    }
}
