using System;
using System.Collections.Generic;
using System.Linq;
using PushkaGraph.Core;

namespace PushkaGraph.Tools
{
    public static class GraphTools
    {
        public static int[,] GetAdjacencyMatrix(this Graph graph)
        {
            var adjacencyMatrix = new int[graph.Vertices.Length, graph.Vertices.Length];

            for (var i = 0; i < graph.Vertices.Length; i++)
            {
                for (var j = 0; j < graph.Vertices.Length; j++)
                {
                    adjacencyMatrix[i, j] =
                        graph.Vertices[j].IsAdjacentTo(graph.Vertices[i])
                        ? graph.Vertices[j].GetEdgeBy(graph.Vertices[i]).Weight
                        : 0;
                }
            }

            return adjacencyMatrix;
        }
        
        public static void DeleteVertices(this Graph graph, IEnumerable<Vertex> verticesToDelete)
        {
            foreach (var vertexToDelete in verticesToDelete)
                graph.DeleteVertex(vertexToDelete);
        }

        public static void DeleteEdges(this Graph graph, IEnumerable<Edge> edgesToDelete)
        {
            foreach (var edgeToDelete in edgesToDelete)
                graph.DeleteEdge(edgeToDelete);
        }

        public static void DeleteEdges(this Graph graph, IEnumerable<Tuple<Vertex, Vertex>> verticesPairs)
        {
            foreach (var verticesPair in verticesPairs)
                graph.DeleteEdge(verticesPair.Item1, verticesPair.Item2);
        }

        public static void CleanEdges(this Graph graph) =>
            graph.DeleteEdges(graph.Edges);

        public static void CleanVertices(this Graph graph) =>
            graph.DeleteVertices(graph.Vertices);
    }
}
