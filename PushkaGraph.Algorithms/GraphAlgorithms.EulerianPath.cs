using System.Collections.Generic;
using PushkaGraph.Core;
using System.Linq;
using System;

namespace PushkaGraph.Algorithms
{
    public static partial class GraphAlgorithms
    {
        /*  An algorithm to find eulerian path in graph.
         * 
         *  Case 1. A graph has an eulerian cycle only in case if
         *     1) graph is connected (or has more than one non-empty components)
         *     2) all the vertices has an even degree number
         *  Case 2. A graph has an eulerian path only in case if
         *     1) graph is connected (or has more than one non-empty components)
         *     2) it has at most two vertices with odd degrees
         *
         *  For the case 1:
         *     1) start from any vertex
         *     2) go to another vertex by any unused edge
         *     3) if current vertex hasn't any unused incident edges then rollback to previous vertex
         *
         *  For the case 2:
         *     1) add a fictive edge between two vertices with odd degrees
         *     2) solve as in case 1
         *     3) break the found cycle by deletion of the fictive edge 
         */
        public static IEnumerable<Edge> EulerianPath(this Graph graph)
        {
            if (graph.Vertices.Length == 0)
                return null;

            var oddDegreeVertices = graph.Vertices.Where(v => v.IncidentEdges.Length % 2 != 0).ToList();

            // The graph with more than two odd degree vertices can't have eulerian path
            if (oddDegreeVertices.Count != 2 && oddDegreeVertices.Count != 0)
                return null;

            Tuple<Vertex, Vertex> fictiveEdge = null;

            if (oddDegreeVertices.Count == 2)
                fictiveEdge = Tuple.Create(oddDegreeVertices[0], oddDegreeVertices[1]);

            var stack = new Stack<Vertex>();
            var usedEdges = new HashSet<Edge>();
            var usedFictiveEdge = false;

            // Have to split path into two parts: before the use of fictive edge and after
            var eulerianPathPart1 = new List<Edge>();
            var eulerianPathPart2 = new List<Edge>();

            // Start with any vertex
            stack.Push(graph.Vertices.First());

            while (stack.Count != 0)
            {
                var currentVertex = stack.Peek();

                // Try to use a fictive edge first (if it's present)
                if (fictiveEdge != null)
                    if (fictiveEdge.Item1 == currentVertex && !usedFictiveEdge)
                    {
                        stack.Push(fictiveEdge.Item2);
                        usedFictiveEdge = true;
                        continue;
                    } else if (fictiveEdge.Item2 == currentVertex && !usedFictiveEdge)
                    {
                        stack.Push(fictiveEdge.Item1);
                        usedFictiveEdge = true;
                        continue;
                    }

                // Find first unused incident edge
                foreach (var edge in currentVertex.IncidentEdges)
                {
                    if (usedEdges.Contains(edge))
                        continue;

                    stack.Push(edge.GetAdjacentVertexTo(currentVertex));

                    // Decide which part of the path is active now
                    if (!usedFictiveEdge)
                        eulerianPathPart1.Add(edge);
                    else
                        eulerianPathPart2.Add(edge);

                    usedEdges.Add(edge);

                    break;
                }

                // If didn't find any unused edge - rollback
                if (stack.Peek() == currentVertex)
                    stack.Pop();
            }

            /*  The eulerian cycle now is:
                eulerianPathPart1 + fictiveEdge(if present) + eulerianPathPart2
                To break the cycle by fictive edge we need to add all the edges of part 2 before part1 in reveres order
            */
            
            eulerianPathPart2.Reverse();

            var eulerianPath = eulerianPathPart2;
            eulerianPath.AddRange(eulerianPathPart1);

            // Return null in case some edges weren't reached (in case of disconnected graphs)
            return eulerianPath.Count == graph.Edges.Length ? eulerianPath : null;
        }
    }
}
