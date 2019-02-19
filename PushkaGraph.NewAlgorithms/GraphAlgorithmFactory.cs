using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using PushkaGraph.NewAlgorithms.Wrapper;

namespace PushkaGraph.NewAlgorithms
{
    public static class GraphAlgorithmFactory
    {
        public static GraphAlgorithm ResolveGraphAlgorithm(string algorithmName)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var types = currentAssembly.GetTypes();
            var algorithmTypes = types.Where(t => t.BaseType == typeof(GraphAlgorithm));
            var algorithmType = algorithmTypes.FirstOrDefault(a => a.Name == algorithmName);

            if (algorithmType == null)
                throw new ArgumentException("Алгоритма с таким названием нет в сборке");

            var constructor = algorithmType.GetConstructor(new Type[0]);
            var algorithm = constructor.Invoke(new object[0]) as GraphAlgorithm;

            return algorithm;
        }
        
        public static GraphAlgorithm[] CreateAllGraphAlgorithms()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var types = currentAssembly.GetTypes();
            var algorithmTypes = types.Where(t => t.BaseType == typeof(GraphAlgorithm)).ToArray();

            var algorithms = new List<GraphAlgorithm>();

            foreach (var algorithmType in algorithmTypes)
            {
                var constructor = algorithmType.GetConstructor(new Type[0]);
                var algorithm = constructor.Invoke(new object[0]) as GraphAlgorithm;
                algorithms.Add(algorithm);
            }

            return algorithms.ToArray();
        }
    }
}
