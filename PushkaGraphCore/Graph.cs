using System;
using System.Collections.Generic;

namespace PushkaGraphCore
{
    public class Graph
    {
        private List<Vertex> _vertices;
        private List<Edge> _edges;

        public Vertex[] Vertices => _vertices.ToArray();
        public Edge[] Edges => _edges.ToArray();

        public Graph(int verticesCount = 0)
        {
            _vertices = new List<Vertex>(verticesCount);
            _edges = new List<Edge>();

            AddVertices(verticesCount);
        }
        
        public Vertex AddVertex()
        {
            var vertex = new Vertex();
            _vertices.Add(vertex);

            return vertex;
        }

        public IEnumerable<Vertex> AddVertices(int count)
        {
            var vertices = new Vertex[count];
            for (var i = 0; i < count; i++)
                vertices[0] = AddVertex();

            return vertices;
        }

        public void DeleteVertex(Vertex vertex)
        {
            throw new NotImplementedException();
        }

        public void AddEdge(Edge edge)
        {
            throw new NotImplementedException();
        }

        public Edge AddEdge(Vertex firstVertex, Vertex secondVertex)
        {
            throw new NotImplementedException();
        }

        public void DeleteEdge(Edge edge)
        {
            throw new NotImplementedException();
        }

        public void DeleteEdge(Vertex firstVertex, Vertex secondVertex)
        {
            throw new NotImplementedException();
        }

    }
}
