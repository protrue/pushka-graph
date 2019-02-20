using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PushkaGraph.Algorithms;
using PushkaGraph.Core;
using PushkaGraph.NewAlgorithms;
using PushkaGraph.NewAlgorithms.Wrapper;

namespace PushkaGraph.Tests
{
    [TestClass]
    public class NewAlgorithmsTests
    {
        [TestMethod]
        public void ConnectedComponentsCountTest()
        {
            var graph = new Graph(3);
            var algorithm = GraphAlgorithmFactory.ResolveGraphAlgorithm(GraphAlgorithmsRegistry.ConnectedComponentsCount);
            var parameters = new GraphAlgorithmParameters(graph);
            algorithm.PerformAlgorithmAsync(parameters);

            while (algorithm.IsPerforming)
                Thread.Sleep(10);

            algorithm.Result.Number.Should().Be(3);
        }
    }
}
