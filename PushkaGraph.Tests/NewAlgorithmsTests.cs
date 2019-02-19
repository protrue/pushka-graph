using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PushkaGraph.Core;
using PushkaGraph.NewAlgorithms;

namespace PushkaGraph.Tests
{
    [TestClass]
    public class NewAlgorithmsTests
    {
        [TestMethod]
        public void SomeNewAlgorithmsTest()
        {
            var graph = new Graph(1);
            var algorithm = GraphAlgorithmFactory.ResolveGraphAlgorithm(GraphAlgorithmsRegistry.ExampleAlgorithm);
            var parameters = new GraphAlgorithmParameters(graph, edges: new Edge[] { });
            algorithm.PerformAlgorithmAsync(parameters);
            algorithm.AlgorithmPerformed += OnAlgorithmPerformed;
        }

        private void OnAlgorithmPerformed(GraphAlgorithmResult result)
        {

        }
    }
}
