using System.Collections.Generic;

namespace PushkaGraphCore
{
    public partial class Graph
    {
        private class Vertex : IVertex
        {
            public List<Vertex> AdjacentVerticesList;
            public List<Edge> IncidentVerticesList;

            public IVertex[] AdjacentVertices => AdjacentVerticesList.ToArray();
            public Edge[] IncidentEdges => IncidentVerticesList.ToArray();
            
            public Vertex() : this(new Vertex[0]) { }
            
            /// <summary>
            /// Инициализирует вершину
            /// </summary>
            /// <param name="adjacentVertices">Соседние вершины</param>
            public Vertex(IEnumerable<Vertex> adjacentVertices)
            {
                AdjacentVerticesList = new List<Vertex>(adjacentVertices);
                IncidentVerticesList = new List<Edge>();
                foreach (var vertex in AdjacentVertices)
                    IncidentVerticesList.Add(new Edge(this, vertex));
            }
        }
    }
}
