using PushkaGraphCore;

namespace PushkaGraphTools
{
    public static class GraphTools
    {
        public static int[,] GetAdjacencyMatrix(this Graph graph)
        {
            var adjacencyMatrix = new int[graph.Vertices.Length, graph.Vertices.Length];

            for (var i = 0; i < graph.Vertices.Length; i++)
            {
                for (var j = 0; j < graph.Vertices.Length; j++)
                {
                    adjacencyMatrix[i,j] =
                        graph.Vertices[j].IsAdjacentTo(graph.Vertices[i])
                        ? graph.Vertices[j].GetEdgeBy(graph.Vertices[i]).Weight
                        : 0;
                }
            }

            return adjacencyMatrix;
        }
    }
}
