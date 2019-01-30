using System;
using System.Collections.Generic;
using System.Linq;

namespace PushkaGraphCore
{
    public class Vertex
    {
        private readonly List<Vertex> _adjacentVertices;
        private readonly List<Edge> _incidentEdges;

        public Vertex[] AdjacentVertices => _adjacentVertices.ToArray();
        public Edge[] IncidentEdges => _incidentEdges.ToArray();

        /// <summary>
        /// Инициализирует вершину
        /// </summary>
        /// <param name="adjacentVertices">Соседние вершины</param>
        public Vertex(IEnumerable<Vertex> adjacentVertices = null)
        {
            if (adjacentVertices == null)
                adjacentVertices = new Vertex[0];

            _adjacentVertices = new List<Vertex>(adjacentVertices);
            _incidentEdges = new List<Edge>();
            foreach (var vertex in AdjacentVertices)
                _incidentEdges.Add(new Edge(this, vertex));
        }

        internal void AddAdjacentVertex(Vertex vertex)
        {
            vertex.AddAdjacentVertex(this);
            _adjacentVertices.Add(vertex);
            _incidentEdges.Add(new Edge(this, vertex));
        }

        internal void AddIncidentEdge(Edge edge)
        {
            if (edge.FirstVertex != this && edge.SecondVertex != this)
                throw new ArgumentException();

            var adjacentVertex = edge.FirstVertex == this ? edge.SecondVertex : edge.FirstVertex;

            _adjacentVertices.Add(adjacentVertex);
            _incidentEdges.Add(edge);
        }

        internal void RemoveAdjacentVertex(Vertex vertex)
        {
            throw new NotImplementedException();
        }

        internal void RemoveIncidentEdge(Edge edge)
        {
            throw new NotImplementedException();
        }
    }
}
