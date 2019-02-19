using System;
using PushkaGraph.Core;
using PushkaGraph.NewAlgorithms.Wrapper;

namespace PushkaGraph.NewAlgorithms.Implementations
{
    class ExampleAlgorithm : GraphAlgorithm
    {
        public override string Name => "Example algorithm";
        public override string Description => "Some description of example algorithm";
        public override Type[] RequiredParameterTypes => new[] { typeof(Graph), typeof(Edge[]) };
        public override Type[] ResultTypes => new[] { typeof(Vertex[]), typeof(string) };
        
        private Vertex[] ExampleAlgorithmCore(Graph graph, Edge[] edges)
        {
            return new Vertex[0];
        }

        protected override GraphAlgorithmResult PerformAlgorithm(GraphAlgorithmParameters parameters)
        {
            var graph = parameters.Graph;
            var edges = parameters.Edges;

            var vertices = ExampleAlgorithmCore(graph, edges);

            var result = new GraphAlgorithmResult(vertices: vertices);

            return result;
        }
    }
   
}
