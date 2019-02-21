using System;
using System.Linq;
using PushkaGraph.Algorithms;
using PushkaGraph.Core;
using PushkaGraph.NewAlgorithms.Wrapper;

namespace PushkaGraph.NewAlgorithms.Implementations
{
    class MinimumSpanningTree : GraphAlgorithm
    {
        // Указываем название алгоритма и его описание, которые будут отображаться на интерфейсе
        public override string Name => "Минимальное остовное дерево";
        public override string Description => "Поиск минимального остовного дерева в графе";

        // Указываем типы, которые алгоритм принимает на вход как аргументы
        public override Tuple<Type, int>[] RequiredParameters => new[] { Tuple.Create(typeof(Graph), 1) };

        // Указываем типы, которые алгоритм возвращает как результат своего выполнения
        public override Type[] ResultTypes => new[] { typeof(Edge[]) };

        protected override GraphAlgorithmResult PerformAlgorithm(GraphAlgorithmParameters parameters)
        {
            // Извлекаем из параметров нужные штуки
            var graph = parameters.Graph;

            // Вызываем написанный extension метод
            var minimumSpanningTree = GraphAlgorithms.MST(graph);

            var stringResult =
                minimumSpanningTree != null
                ? $"Минимальное остновное дерево: {string.Join<Edge>(Environment.NewLine, minimumSpanningTree.ToArray())}"
                : "Для данного графа невозможно построить миниамльное остовное дерево";

            // Оборачиваем результат выполнения
            var result = new GraphAlgorithmResult(edges: minimumSpanningTree?.ToArray());

            // Возвращаем
            return result;
        }
    }
}
