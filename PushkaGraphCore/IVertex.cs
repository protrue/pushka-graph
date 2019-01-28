namespace PushkaGraphCore
{
    public interface IVertex
    {
        IVertex[] AdjacentVertices { get; }
        Edge[] IncidentEdges { get; }
    }
}
