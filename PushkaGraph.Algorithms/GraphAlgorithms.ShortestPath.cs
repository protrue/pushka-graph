using PushkaGraph.Core;
using System.Collections.Generic;

namespace PushkaGraph.Algorithms
{
    public static partial class GraphAlgorithms
    {
        public static IEnumerable<Vertex> ShortestPath(this Graph graph, Vertex begin, Vertex end)
        {
            if (begin == null || end == null || graph.Edges.Length == 0 || begin.IncidentEdges.Length == 0 || end.IncidentEdges.Length == 0)
                return null;            
            var previous = new Dictionary<Vertex, Vertex>();
            var distances = new Dictionary<Vertex, int>();
            var nodes = new List<Vertex>();

            List<Vertex> path = null;

            foreach (var vertex in graph.Vertices)
            {
                if (vertex == begin)
                {
                    distances[vertex] = 0;
                }
                else
                {
                    distances[vertex] = int.MaxValue;
                }

                nodes.Add(vertex);
            }

            while (nodes.Count != 0)
            {
                nodes.Sort((x, y) => distances[x] - distances[y]);

                var smallest = nodes[0];
                nodes.Remove(smallest);

                if (smallest == end)
                {
                    path = new List<Vertex>();
                    while (previous.ContainsKey(smallest))
                    {
                        path.Add(smallest);
                        smallest = previous[smallest];
                    }

                    break;
                }

                if (distances[smallest] == int.MaxValue)
                {
                    break;
                }
                foreach (var neighbor in smallest.AdjacentVertices)
                {
                    var alt = distances[smallest] + neighbor.GetEdgeBy(smallest).Weight;
                    if (alt < distances[neighbor])
                    {
                        distances[neighbor] = alt;
                        previous[neighbor] = smallest;
                    }
                }
            }
            if (path != null)
            {
                path.Add(begin);
                path.Reverse();
            }
            return path;
        }
    }
}