using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PushkaGraph.NewAlgorithms
{
    public static class GraphAlgorithmFactory
    {
        public static GraphAlgorithm ResolveGraphAlgorithm(string algorithmName)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var types = currentAssembly.GetTypes();
            var algorithms = types.Where(t => t.BaseType == typeof(GraphAlgorithm));
            var algorithm = algorithms.FirstOrDefault(a => a.Name == algorithmName);

            if (algorithm == null)
                throw new ArgumentException("Нет алгоритма с таким названием");

            var constructor = algorithm.GetConstructor(new Type[0]);
            var algorithmObject = constructor.Invoke(new object[0]) as GraphAlgorithm;

            return algorithmObject;
        }
    }
}
