using System;
using System.Collections.Generic;

namespace PushkaGraphCore
{
    public partial class Graph
    {
        private List<Vertex> _vertices;
        private List<Edge> _edges;

        public IVertex[] Vertices => _vertices.ToArray();
        public Edge[] Edges => _edges.ToArray();

        public Graph(int verticesCount = 0)
        {
            _vertices = new List<Vertex>(verticesCount);
            _edges = new List<Edge>();

            AddVertices(verticesCount);
        }
        
        public IVertex AddVertex()
        {
            var vertex = new Vertex();
            _vertices.Add(vertex);

            return vertex;
        }

        public IEnumerable<IVertex> AddVertices(int count)
        {
            var vertices = new IVertex[count];
            for (var i = 0; i < count; i++)
                vertices[0] = AddVertex();

            return vertices;
        }

        public Edge AddEdge(IVertex firstVertex, IVertex secondVertex)
        {
            throw new NotImplementedException();
        }

        public void DeleteVertex(IVertex vertex)
        {
            throw new NotImplementedException();
        }

        public void DeleteEdge(Edge edge)
        {
            throw new NotImplementedException();
        }

        public void DeleteEdge(IVertex firstVertex, IVertex secondVertex)
        {
            throw new NotImplementedException();
        }

    }
}
