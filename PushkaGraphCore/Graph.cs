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

        public Graph()
        {

        }
        
        public IVertex AddVertex()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IVertex> AddVertices(int count)
        {
            throw new NotImplementedException();
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
