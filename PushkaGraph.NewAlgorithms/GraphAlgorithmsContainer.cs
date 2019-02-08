using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushkaGraph.NewAlgorithms
{
    public static class GraphAlgorithmsContainer
    {
        public static IGraphAlgorithm[] GraphAlgorithms { get; }

        public static IGraphAlgorithm ResolveGraphAlgorithm(string algorithmName)
        {
            var algorithm = GraphAlgorithms.FirstOrDefault(a => a.Name == algorithmName);

            if (algorithm == null)
                throw new ArgumentOutOfRangeException(nameof(algorithmName),algorithmName,
                    "Невозможно разрешить");

            return algorithm;
        }

        static GraphAlgorithmsContainer()
        {
            GraphAlgorithms = new IGraphAlgorithm[]
            {
                new ExampleAlgorithm()
            };
        }
    }
}
