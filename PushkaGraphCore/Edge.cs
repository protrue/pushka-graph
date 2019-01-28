namespace PushkaGraphCore
{
    public class Edge
    {
        public int Weight { get; set; }
        public IVertex FirstVertex { get; }
        public IVertex SecondVertex { get; }

        public Edge(IVertex firstVertex, IVertex secondVertex, int weight = 0)
        {
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
            Weight = weight;
        }
    }
}
