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

            if (this.FirstVertex == vertex)
                adjacentVertex = this.SecondVertex;

            if (this.SecondVertex == vertex)
                adjacentVertex = this.FirstVertex;

            return adjacentVertex;
        }
    }
}
