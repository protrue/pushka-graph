namespace PushkaGraphCore
{
    public class Edge
    {
        public int Weight { get; set; }
        public Vertex FirstVertex { get; }
        public Vertex SecondVertex { get; }

        public Edge(Vertex firstVertex, Vertex secondVertex, int weight = 0)
        {
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
            Weight = weight;
        }
    }
}
