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
         *     3) if current vertex hasn't any unused incident edges then rollback to previous vertex and add current to answer
         *
         *  For the case 2:
         *     1) add a fictive edge between two vertices with odd degrees
         *     2) solve as in case 1, but start in add degree vertex
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

            var verticies = new List<Vertex>();

            // Start with any vertex
            stack.Push(oddDegreeVertices.Count == 0 ? graph.Vertices.First() : oddDegreeVertices.First());

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

                    usedEdges.Add(edge);

                    break;
                }

                // If didn't find any unused edge - rollback
                if (stack.Peek() == currentVertex)
                {
                    stack.Pop();
                    verticies.Add(currentVertex);
                }
            }
            
            var eulerianPath = new List<Edge>();
            
            for (int i = 1; i < verticies.Count; i++)
            {
                eulerianPath.Add(graph.Edges.First(x => x.IsIncidentTo(verticies[i - 1]) && x.IsIncidentTo(verticies[i])));
            }
            
            if (oddDegreeVertices.Count > 0)
                eulerianPath.RemoveAt(eulerianPath.Count - 1);
            
            // Return null in case some edges weren't reached (in case of disconnected graphs)
            return eulerianPath.Count == graph.Edges.Length ? eulerianPath : null;
        }
    }
}
