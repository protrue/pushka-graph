using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PushkaGraph.NewAlgorithms
{
    public static class GraphAlgorithmsContainer
    {
        public static IGraphAlgorithm[] GraphAlgorithms { get; }

        public static IGraphAlgorithm ResolveGraphAlgorithm(string algorithmName)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var types = currentAssembly.GetTypes();
            var algorithms = types.Where(t => t.GetInterfaces().Contains(typeof(IGraphAlgorithm)));
            var algorithm = algorithms.FirstOrDefault(a => a.Name == algorithmName);

            var constructor = algorithm.GetConstructor(new Type[0]);

            var algorithmObject = Convert.ChangeType(constructor.Invoke(new object[0]), algorithm.DeclaringType);

            return algorithmObject;
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
