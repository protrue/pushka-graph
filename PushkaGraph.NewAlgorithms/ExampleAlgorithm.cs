using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PushkaGraph.Core;

namespace PushkaGraph.NewAlgorithms
{
    class ExampleAlgorithm : IGraphAlgorithm
    {
        public string Name => "Example algorithm";
        public string Description => "Some description of example algorithm";
        public Type[] ParameterTypes => new[] { typeof(Graph), typeof(Edge[]) };
        public Type[] ResultTypes => new[] { typeof(Vertex[]), typeof(string) };


        private Vertex[] ExampleAlgorithmCore(Graph graph, Edge[] edges)
        {
            throw new NotImplementedException();
        }

        public GraphAlgorithmResult PerformAlgorithm(GraphAlgorithmParameters parameters)
        {
            var graph = parameters.Graph;
            var edges = parameters.Edges;

            var vertices = ExampleAlgorithmCore(graph, edges);

            var result = new GraphAlgorithmResult(vertices: vertices);
            return result;
        }
    }
}
