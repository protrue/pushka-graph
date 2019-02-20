using PushkaGraph.Core;
using System.Collections.Generic;

namespace PushkaGraph.Algorithms
{
    public static partial class GraphAlgorithms
    {
        public static IEnumerable<Vertex> ShortestPath(this Graph graph, Vertex begin, Vertex end)
        {
            List<Vertex> path = new List<Vertex>();
            path.Add(begin);
            Vertex current = begin;
            Vertex last;
            int minimalpath = 0;
            while (current != end)
            {
                int length = current.IncidentEdges.Length;
                int curpath = minimalpath + current.IncidentEdges[0].Weight;
                int currentId = 0;
                for (int i = 1; i < length; i++)
                    if (minimalpath + current.IncidentEdges[i].Weight < curpath)
                    {
                        curpath = minimalpath + current.IncidentEdges[i].Weight;
                        currentId = i;
                    }
                minimalpath = curpath;
                last = current;
                current = current.GetAdjacentVertexBy(current.IncidentEdges[currentId]);
                path.Add(current);
            }
            return path;
        }
    }
}