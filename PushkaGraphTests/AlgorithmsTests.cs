using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PushkaGraphCore;
using PushkaGraphAlgorithms;

namespace PushkaGraphTests
{
    [TestClass]
    public class AlgorithmsTests
    {
        [TestMethod]
        public void ExampleExtensionMethodShouldDoNothing()
        {
            var graph = new Graph();

            graph.SomeAlgorithm(null).Should().BeNull();
        }
    }
}
