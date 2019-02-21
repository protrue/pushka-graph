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
            Tuple.Create(typeof(Vertex), 1)
        };

        // Указываем типы, которые алгоритм возвращает как результат своего выполнения
        public override Type[] ResultTypes => new[] { typeof(Vertex[]) };

        protected override GraphAlgorithmResult PerformAlgorithm(GraphAlgorithmParameters parameters)
        {
            // Извлекаем из параметров нужные штуки
            var graph = parameters.Graph;
            var begin = graph.Vertices[0];
            var end = graph.Vertices[1];

            // Вызываем написанный extension метод
            var shortestPath = graph.ShortestPath(begin, end)?.ToArray();

            var stringResult =
                shortestPath != null
                ? $"Кратчайший путь: {string.Join<Vertex>(Environment.NewLine, shortestPath)}"
                : "Нет пути";

            // Оборачиваем результат выполнения
            var result = new GraphAlgorithmResult(vertices: shortestPath, stringResult: stringResult);

            // Возвращаем
            return result;
        }
    }
}
