using System.Collections.Generic;
using System.Linq;

namespace PushkaGraph.Core
{
    public class Vertex
    {
        private readonly HashSet<Vertex> _adjacentVertices;
        private readonly HashSet<Edge> _incidentEdges;

        public int Index { get; internal set; }
        public Vertex[] AdjacentVertices => _adjacentVertices.ToArray();
        public Edge[] IncidentEdges => _incidentEdges.ToArray();

        internal Vertex(int index)
        {
            Index = index;
            _adjacentVertices = new HashSet<Vertex>();
            _incidentEdges = new HashSet<Edge>();
        }

        public Vertex GetAdjacentVertexBy(Edge edge)
        {
            Vertex adjacentVertex = null;

            if (edge.FirstVertex == this)
                adjacentVertex = edge.SecondVertex;

            if (edge.SecondVertex == this)
                adjacentVertex = edge.FirstVertex;

            return adjacentVertex;
        }

        public Edge GetEdgeBy(Vertex adjacentVertex) =>
            _incidentEdges.FirstOrDefault(e => e.IsIncidentTo(this)
                                               && e.IsIncidentTo(adjacentVertex));

        public bool IsAdjacentTo(Vertex otherVertex) =>
            _adjacentVertices.Contains(otherVertex);

        internal void AddAdjacentVertex(Vertex vertex) =>
            _adjacentVertices.Add(vertex);

        internal void DeleteAdjacentVertex(Vertex vertex) =>
            _adjacentVertices.Remove(vertex);

        internal void AddIncidentEdge(Edge edge) =>
            _incidentEdges.Add(edge);

        internal void DeleteIncidentEdge(Edge edge) =>
            _incidentEdges.Remove(edge);

        public override string ToString() =>
            $"{Index} [{string.Join(" ", _adjacentVertices.Select(v => v.Index))}]";
    }
}