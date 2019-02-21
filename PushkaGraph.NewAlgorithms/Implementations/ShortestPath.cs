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
    class ShortestPath : GraphAlgorithm
    {
        public override string Name => "Кратчайший путь";
        public override string Description => "Поиск кратчайшего пути между точками в графе";

        // Указываем типы, которые алгоритм принимает на вход как аргументы
        public override Type[] RequiredParameterTypes => new[] { typeof(Graph), typeof(Vertex[]) };

        // Указываем типы, которые алгоритм возвращает как результат своего выполнения
        public override Type[] ResultTypes => new[] { typeof(IEnumerable<Vertex>) };

        protected override GraphAlgorithmResult PerformAlgorithm(GraphAlgorithmParameters parameters)
        {
            // Извлекаем из параметров нужные штуки
            var graph = parameters.Graph;
            var begin = graph.Vertices[0];
            var end = graph.Vertices[4];

            // Вызываем написанный extension метод
            var VertexPath = GraphAlgorithms.ShortestPath(graph,begin,end);

            // Оборачиваем результат выполнения
            var result = new GraphAlgorithmResult(vertices: VertexPath.ToArray());

            // Возвращаем
            return result;
        }
    }
}
