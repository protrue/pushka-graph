using System;
using PushkaGraph.Algorithms;
using PushkaGraph.Core;
using PushkaGraph.NewAlgorithms.Wrapper;

namespace PushkaGraph.NewAlgorithms.Implementations
{
    // Наследуемся от абстрактного GraphAlgorithm
    public class ConnectedComponentsCount : GraphAlgorithm
    {
        // Указываем название алгоритма и его описание, которые будут отображаться на интерфейсе
        public override string Name => "Количество компонент связности";
        public override string Description => "Поиск количества компонент связности в графе";

        // Указываем типы, которые алгоритм принимает на вход как аргументы
        public override Tuple<Type, int>[] RequiredParameters => new[] { Tuple.Create(typeof(Graph), 1) };

        // Указываем типы, которые алгоритм возвращает как результат своего выполнения
        public override Type[] ResultTypes => new[] { typeof(int) };

        protected override GraphAlgorithmResult PerformAlgorithm(GraphAlgorithmParameters parameters)
        {
            // Извлекаем из параметров нужные штуки
            var graph = parameters.Graph;

            // Вызываем написанный extension метод
            var connectedComponentsCount = GraphAlgorithms.ConnectedComponentsCount(graph);

            // Оборачиваем результат выполнения
            var result = new GraphAlgorithmResult(number: connectedComponentsCount);

            // Возвращаем
            return result;
        }
    }
}
