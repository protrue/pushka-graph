using System.Collections.Generic;
using System;
using System.Linq;
using PushkaGraph.Core;
namespace PushkaGraph.Algorithms
{
    public static partial class GraphAlgorithms
    {
        public static Edge minEdgeForAllUsedVertices(List<Vertex> usedVerticesList, List<Edge> usedEdgesList, Graph graph)
        {
            double minEdge = double.MaxValue;
            Edge edge = null;
            for (int i = 0; i < usedVerticesList.Count; i++)
            {
                var edges = usedVerticesList[i].IncidentEdges;
                for (int j = 0; j < edges.Length; j++)
                {
                    if ((edges[j].Weight < minEdge) && !usedEdgesList.Contains(edges[j]) && (!usedVerticesList.Contains(edges[j].FirstVertex) || !usedVerticesList.Contains(edges[j].SecondVertex)))
                    {
                        minEdge = edges[j].Weight;
                        edge = edges[j];
                    }
                }
            }
            return edge;
        }

        public static IEnumerable<Edge> MST(this Graph graph)
        {   //Реализация алгоритма Прима

            if (GraphAlgorithms.ConnectedComponentsCount(graph) != 1 || graph.Vertices.Length == 1) return null; //компонента связности отличная от 1 - искать ничего не будем

            Vertex[] unusedVertices = graph.Vertices; //получили все вершины графа
            var usedVerticesList = new List<Vertex>(unusedVertices.Length); //список использованных вершин
            var unusedVerticesList = (unusedVertices).OfType<Vertex>().ToList();
            var answer = new List<Edge>(unusedVertices.Length - 1); //ответ (список ребер) ,количество ребер на 1 меньше чем вершин (1)
            usedVerticesList.Add(unusedVertices[0]); // добавили 1-ую вершину( нам без разницы какая она)

            for (int i = 0; i < unusedVertices.Length - 1; i++) //выполним n-1 раз см. (1)
            {
                var minEdge = minEdgeForAllUsedVertices(usedVerticesList, answer, graph);
                answer.Add(minEdge);
                if (!usedVerticesList.Contains(minEdge.FirstVertex)) usedVerticesList.Add(minEdge.FirstVertex);
                else usedVerticesList.Add(minEdge.SecondVertex);
            }
            return answer;
        }
    }
}