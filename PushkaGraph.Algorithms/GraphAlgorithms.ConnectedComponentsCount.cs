using PushkaGraph.Core;
using System.Collections.Generic;

namespace PushkaGraph.Algorithms
{
    public static partial class GraphAlgorithms
    {
        public static void DFS(this Graph graph, Vertex currentVertex, bool[] visited)
        {
            visited[currentVertex.Index - 1] = true;

            foreach (var adjacentVertex in currentVertex.AdjacentVertices)
                if (!visited[adjacentVertex.Index - 1])
                    DFS(graph, adjacentVertex, visited);
        }

        public static int ConnectedComponentsCount(this Graph graph)
        {
            int result = 0;
            var visited = new bool[graph.Vertices.Length];

            foreach (var vertex in graph.Vertices)
                if (!visited[vertex.Index - 1])
                {
                    DFS(graph, vertex, visited);
                    result++;
                }

            return result;
        }
    }
}