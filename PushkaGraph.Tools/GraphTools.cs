using System;
using System.Collections.Generic;
using PushkaGraph.Core;

namespace PushkaGraph.Tools
{
    public static class GraphTools
    {
        public static int[,] GetAdjacencyMatrix(this Graph graph)
        {
            var adjacencyMatrix = new int[graph.Vertices.Length, graph.Vertices.Length];

            for (var i = 0; i < graph.Vertices.Length; i++)
                for (var j = 0; j < graph.Vertices.Length; j++)
                {
                    adjacencyMatrix[i, j] =
                        graph.Vertices[j].IsAdjacentTo(graph.Vertices[i])
                            ? graph.Vertices[j].GetEdgeBy(graph.Vertices[i]).Weight
                            : 0;
                }

            return adjacencyMatrix;
        }

        private static bool IsValidAdjacencyMatrix(int[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                return false;

            for (var i = 0; i < matrix.GetLength(0); i++)
                for (var j = i + 1; j < matrix.GetLength(0); j++)
                    if (matrix[j, i] < 0 || matrix[j, i] != matrix[i, j])
                        return false;

            return true;
        }

        public static void CreateFromAdjacencyMatrix(this Graph graph, int[,] adjacencyMatrix)
        {
            if (adjacencyMatrix == null)
                throw new ArgumentNullException(nameof(adjacencyMatrix), "Матрица смежности была null");

            if (!IsValidAdjacencyMatrix(adjacencyMatrix))
                throw new ArgumentException("Некорректная матрица смежности для неориентированного графа",
                    nameof(adjacencyMatrix));

            graph.CleanVertices();

            var vertices = graph.AddVertices(adjacencyMatrix.GetLength(0));

            for (var i = 0; i < adjacencyMatrix.GetLength(0); i++)
                for (var j = i + 1; j < adjacencyMatrix.GetLength(0); j++)
                    if (adjacencyMatrix[i, j] > 0)
                        graph.AddEdge(vertices[i], vertices[j], adjacencyMatrix[i, j]);
        }

        public static Vertex[] AddVertices(this Graph graph, int count)
        {
            var vertices = new Vertex[count];
            for (var i = 0; i < count; i++)
                vertices[i] = graph.AddVertex();

            return vertices;
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
