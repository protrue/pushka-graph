using System;
using System.Collections.Generic;
using System.Linq;

namespace PushkaGraph.Core
{
    public class Graph
    {
        private readonly HashSet<Vertex> _vertices;
        private readonly HashSet<Edge> _edges;

        public Vertex[] Vertices => _vertices.ToArray();
        public Edge[] Edges => _edges.ToArray();

        public Graph(int verticesCount = 0)
        {
            if (verticesCount < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(verticesCount), verticesCount,
                    "Количество вершин должно быть больше нуля");

            _vertices = new HashSet<Vertex>();
            _edges = new HashSet<Edge>();

            for (var i = 1; i <= verticesCount; i++)
                AddVertex();
        }

        public Vertex AddVertex()
        {
            var vertex = new Vertex();

            _vertices.Add(vertex);

            return vertex;
        }

        public void DeleteVertex(Vertex vertex)
        {
            if (vertex == null)
                throw new ArgumentNullException(nameof(vertex),"Вершина была null");

            if (!_vertices.Contains(vertex))
                throw new ArgumentException("Граф не содержит такой вершины");

            _vertices.Remove(vertex);
            _edges.RemoveWhere(e => e.IsIncidentTo(vertex));

            foreach (var adjacentVertex in vertex.AdjacentVertices)
                adjacentVertex.DeleteAdjacentVertex(vertex);
        }

        public Edge AddEdge(Vertex firstVertex, Vertex secondVertex, int weight = 1)
        {
            if (firstVertex == null)
                throw new ArgumentNullException(nameof(firstVertex), "Вершина была null");

            if (secondVertex == null)
                throw new ArgumentNullException(nameof(secondVertex), "Вершина была null");

            if (!_vertices.Contains(firstVertex) || !_vertices.Contains(secondVertex))
                throw new ArgumentException("Одна или обе вершины не принадлежат графу");

            if (firstVertex == secondVertex)
                throw new ArgumentException("Петли недопустимы");

            if (firstVertex.IsAdjacentTo(secondVertex))
                throw new ArgumentException("Кратные рёбра недопустимы");

            var edge = new Edge(firstVertex, secondVertex, weight);

            _edges.Add(edge);

            firstVertex.AddAdjacentVertex(secondVertex);
            secondVertex.AddAdjacentVertex(firstVertex);

            firstVertex.AddIncidentEdge(edge);
            secondVertex.AddIncidentEdge(edge);

            return edge;
        }

        public void DeleteEdge(Edge edge)
        {
            if (edge == null)
                throw new ArgumentNullException(nameof(edge),"Ребро было null");

            if (!_edges.Contains(edge))
                throw new ArgumentException("Граф не содержит такого ребра");

            _edges.Remove(edge);

            edge.FirstVertex.DeleteIncidentEdge(edge);
            edge.SecondVertex.DeleteIncidentEdge(edge);

            edge.FirstVertex.DeleteAdjacentVertex(edge.SecondVertex);
            edge.SecondVertex.DeleteAdjacentVertex(edge.FirstVertex);
        }

        public void DeleteEdge(Vertex firstVertex, Vertex secondVertex)
        {
            if (firstVertex == null)
                throw new ArgumentNullException(nameof(firstVertex),"Вершина была null");

            if (secondVertex == null)
                throw new ArgumentNullException(nameof(secondVertex), "Вершина была null");

            if (!_vertices.Contains(firstVertex) || !_vertices.Contains(secondVertex))
                throw new ArgumentException("Одна или обе вершины не принадлежат графу");

            var edge = firstVertex.GetEdgeBy(secondVertex);

            DeleteEdge(edge);
        }
    }
}