namespace PushkaGraphCore
{
    public class Edge
    {
        public int Weight { get; set; }
        public Vertex FirstVertex { get; }
        public Vertex SecondVertex { get; }

        internal Edge(Vertex firstVertex, Vertex secondVertex, int weight = 0)
        {
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
            Weight = weight;
        }
        
        public bool IsIncidentTo(Vertex vertex) =>
            FirstVertex == vertex || SecondVertex == vertex;

        public Vertex GetAdjacentVertexTo(Vertex vertex)
        {
            Vertex adjacentVertex = null;

            if (FirstVertex == vertex)
                adjacentVertex = SecondVertex;

            if (SecondVertex == vertex)
                adjacentVertex = FirstVertex;

            return adjacentVertex;
        }
    }
}
