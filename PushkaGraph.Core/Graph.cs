﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PushkaGraph.Core
{
    public class Graph
    {
        private readonly HashSet<Vertex> _vertices;
        private readonly HashSet<Edge> _edges;
        private readonly SortedSet<int> _deletedIndexes;

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

            _deletedIndexes = new SortedSet<int>();

            for (var i = 1; i <= verticesCount; i++)
                AddVertex();
        }

        private int GetNextVertexIndex()
        {
            if (_deletedIndexes.Count == 0)
                return _vertices.Count + 1;

            var result = _deletedIndexes.Min;
            _deletedIndexes.Remove(_deletedIndexes.Min);
            return result;
        }

        public Vertex AddVertex()
        {
            var vertex = new Vertex(GetNextVertexIndex());

            _vertices.Add(vertex);

            return vertex;
        }

        public void DeleteVertex(Vertex vertex)
        {
            if (vertex == null)
                throw new ArgumentNullException(nameof(vertex), "Вершина была null");

            if (!_vertices.Contains(vertex))
                throw new ArgumentOutOfRangeException(nameof(vertex), vertex,
                    "Граф не содержит такой вершины");

            _vertices.Remove(vertex);
            _deletedIndexes.Add(vertex.Index);
            _edges.RemoveWhere(e => e.IsIncidentTo(vertex));

            foreach (var adjacentVertex in vertex.AdjacentVertices)
                adjacentVertex.DeleteAdjacentVertex(vertex);
        }

        public Edge AddEdge(Vertex firstVertex, Vertex secondVertex, int weight = 1)
        {
            if (firstVertex == null || secondVertex == null)
                throw new ArgumentNullException(firstVertex == null ? nameof(firstVertex) : nameof(secondVertex),
                    "Вершина была null");

            if (!_vertices.Contains(firstVertex) || !_vertices.Contains(secondVertex))
                throw new ArgumentOutOfRangeException(
                    !_vertices.Contains(firstVertex) ? nameof(firstVertex) : nameof(secondVertex),
                    !_vertices.Contains(firstVertex) ? firstVertex : secondVertex,
                    "Вершина на принадлежит графу");

            if (firstVertex == secondVertex || firstVertex.IsAdjacentTo(secondVertex))
                throw new ArgumentException("Петли и кратные рёбра недопустимы");

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
                throw new ArgumentNullException(nameof(edge), "Ребро было null");

            if (!_edges.Contains(edge))
                throw new ArgumentOutOfRangeException(nameof(edge), edge,
                    "Граф не содержит такого ребра");

            _edges.Remove(edge);

            edge.FirstVertex.DeleteIncidentEdge(edge);
            edge.SecondVertex.DeleteIncidentEdge(edge);

            edge.FirstVertex.DeleteAdjacentVertex(edge.SecondVertex);
            edge.SecondVertex.DeleteAdjacentVertex(edge.FirstVertex);
        }

        public void DeleteEdge(Vertex firstVertex, Vertex secondVertex)
        {
            if (firstVertex == null || secondVertex == null)
                throw new ArgumentNullException(firstVertex == null ? nameof(firstVertex) : nameof(secondVertex),
                    "Вершина была null");

            if (!_vertices.Contains(firstVertex) || !_vertices.Contains(secondVertex))
                throw new ArgumentOutOfRangeException(
                    !_vertices.Contains(firstVertex) ? nameof(firstVertex) : nameof(secondVertex),
                    !_vertices.Contains(firstVertex) ? firstVertex : secondVertex,
                    "Вершина на принадлежит графу");

            var edge = firstVertex.GetEdgeBy(secondVertex);

            DeleteEdge(edge);
        }
    }
}