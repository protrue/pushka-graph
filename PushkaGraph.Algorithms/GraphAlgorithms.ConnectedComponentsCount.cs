using PushkaGraph.Core;
using System.Collections.Generic;

namespace PushkaGraph.Algorithms
{
    public static partial class GraphAlgorithms
    {
        public static void DFS(this Graph graph, Vertex currentVertex, HashSet<int> visited)
        {
            visited.Add(currentVertex.Index);

            foreach (var adjacentVertex in currentVertex.AdjacentVertices)
                if (!visited.Contains(adjacentVertex.Index))
                    DFS(graph, adjacentVertex, visited);
        }

        public static int ConnectedComponentsCount(this Graph graph)
        {
            int result = 0;
            var visited = new HashSet<int>();

            foreach (var vertex in graph.Vertices)
                if (!visited.Contains(vertex.Index))
                {
                    DFS(graph, vertex, visited);
                    result++;
                }

            return result;
        }
    }
}