using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PushkaGraph.Core;

namespace PushkaGraph.NewAlgorithms
{
    public interface IGraphAlgorithm
    {
        string Name { get; }
        string Description { get; }
        Type[] ParameterTypes { get; }
        Type[] ResultTypes { get; }

        GraphAlgorithmResult PerformAlgorithm(GraphAlgorithmParameters parameters);
    }
}
