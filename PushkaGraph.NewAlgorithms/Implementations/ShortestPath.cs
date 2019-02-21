using PushkaGraph.Algorithms;
using PushkaGraph.Core;
using PushkaGraph.NewAlgorithms.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushkaGraph.NewAlgorithms.Implementations
{
    public class ShortestPath : GraphAlgorithm
    {
        public override string Name => "Кратчайший путь";
        public override string Description => "Поиск кратчайшего пути между точками в графе";

        // Указываем типы, которые алгоритм принимает на вход как аргументы
        public override Tuple<Type, int>[] RequiredParameters => new[]
        {
            Tuple.Create(typeof(Graph), 1),
            Tuple.Create(typeof(Vertex), 2)
        };

        // Указываем типы, которые алгоритм возвращает как результат своего выполнения
        public override Type[] ResultTypes => new[] { typeof(IEnumerable<Edge>) };

        protected override GraphAlgorithmResult PerformAlgorithm(GraphAlgorithmParameters parameters)
        {
            // Извлекаем из параметров нужные штуки
            var graph = parameters.Graph;
            var begin = parameters.Vertices[0];
            var end = parameters.Vertices[1];

            // Вызываем написанный extension метод
            var edgePath = graph.ShortestPath(begin, end)?.ToArray();

            var stringResult =
                edgePath != null
                ? $"Кратчайший путь:\n{string.Join<Edge>(Environment.NewLine, edgePath)}"
                : "Нет пути";

            // Оборачиваем результат выполнения
            var result = new GraphAlgorithmResult(edges: edgePath, stringResult: stringResult, isSequential: true);

            // Возвращаем
            return result;
        }
    }
}
