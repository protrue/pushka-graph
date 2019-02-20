using System;
using System.Linq;
using PushkaGraph.Algorithms;
using PushkaGraph.Core;
using PushkaGraph.NewAlgorithms.Wrapper;

namespace PushkaGraph.NewAlgorithms.Implementations
{
    public class EulerianPath : GraphAlgorithm
    {
        public override string Name => "Эйлеров путь";
        public override string Description => "Путь, проходящий по всем рёбрам графа и притом только по одному разу";

        public override Tuple<Type, int>[] RequiredParameters => new[] { Tuple.Create(typeof(Graph), 1) };

        public override Type[] ResultTypes => new[] { typeof(Edge[]) };

        protected override GraphAlgorithmResult PerformAlgorithm(GraphAlgorithmParameters parameters)
        {
            var graph = parameters.Graph;

            var eulerianPath = GraphAlgorithms.EulerianPath(graph);

            var result = new GraphAlgorithmResult(edges: eulerianPath.ToArray());

            return result;
        }
    }
}
