using System;
using System.Collections.Generic;
using System.Linq;

namespace PushkaGraphCore
{
    public class Graph
    {
        private HashSet<Vertex> _vertices;
        private HashSet<Edge> _edges;

        public Vertex[] Vertices => _vertices.ToArray();
        public Edge[] Edges => _edges.ToArray();

        public Graph(int verticesCount = 0)
        {
            _vertices = new HashSet<Vertex>();
            _edges = new HashSet<Edge>();

            AddVertices(verticesCount);
        }
        
        public Vertex AddVertex()
        {
            var vertex = new Vertex();

            _vertices.Add(vertex);

            return vertex;
        }

        public Vertex[] AddVertices(int count)
        {
            var vertices = new Vertex[count];
            for (var i = 0; i < count; i++)
                vertices[0] = AddVertex();

            return vertices;
        }

        public void DeleteVertex(Vertex vertex)
        {
            _vertices.Remove(vertex);
            _edges.RemoveWhere(e => e.IsIncidentTo(vertex));

            foreach (var adjacentVertex in vertex.AdjacentVertices)
                adjacentVertex.DeleteAdjacentVertex(vertex);
        }

        public Edge AddEdge(Vertex firstVertex, Vertex secondVertex, int weight = 0)
        {
            if (!_vertices.Contains(firstVertex) || !_vertices.Contains(secondVertex))
                throw new ArgumentException();

            var edge = new Edge(firstVertex, secondVertex, weight);

            _edges.Add(edge);

            firstVertex.AddAdjacentVertex(secondVertex);
            secondVertex.AddAdjacentVertex(firstVertex);

            return edge;
        }

        public void DeleteEdge(Edge edge)
        {
            _edges.Remove(edge);
            edge.FirstVertex.DeleteIncidentEdge(edge);
            edge.SecondVertex.DeleteIncidentEdge(edge);
        }

        public void DeleteEdge(Vertex firstVertex, Vertex secondVertex)
        {
            var edge = _edges.FirstOrDefault(e =>
                e.FirstVertex == firstVertex
                && e.SecondVertex == secondVertex);

            if (edge == null)
                throw new ArgumentException();

            DeleteEdge(edge);
        }

    }
}
