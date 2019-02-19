using System.Collections.Generic;
using System;
using System.Linq;
using PushkaGraph.Core;
namespace PushkaGraph.Algorithms
{
    public static partial class GraphAlgorithms
    {
        public static Edge minedge_for_allusedvertices(List <Vertex> used_vertices_list, List<Edge> used_edges_list, Graph graph)
        {
            double min_edge = 9999999.9;
            Edge edge=null;
            for (int i=0; i<used_vertices_list.Count; i++)
            {
                var edges = used_vertices_list[i].IncidentEdges;
                for (int j=0; j<used_vertices_list.Count;j++)
                {
                    if(edges[j].Weight<min_edge && !used_edges_list.Contains(edges[j]))
                    {
                        min_edge = edges[j].Weight;
                        edge = edges[j];
                    }
                }
            }
            return edge;
        }
        public static IEnumerable<Edge> MST(this Graph graph)
        {   //Реализация алгоритма Прима
            if( GraphAlgorithms.ConnectedComponentsCount(graph) !=1) return null; //компонента связности отличная от 1 - искать ничего не будем
            Vertex [] unused_vertices = graph.Vertices; //получили все вершины графа
            List <Vertex> used_vertices_list= new List<Vertex>(unused_vertices.Length); //список использованных вершин
            List<Vertex> unused_vertices_list = (unused_vertices).OfType<Vertex>().ToList();
            List < Edge > answer= new List<Edge>(unused_vertices.Length-1); //ответ (список ребер) ,количество ребер на 1 меньше чем вершин (1)
            used_vertices_list.Add(unused_vertices[0]); // добавили 1-ую вершину( нам без разницы какая она)
            for (int i=0; i<unused_vertices.Length-1;i++) //выполним n-1 раз см. (1)
            {
               var minedge = minedge_for_allusedvertices(used_vertices_list, answer, graph);
               answer.Add(minedge);
                if (!used_vertices_list.Contains(minedge.FirstVertex))
                {
                    used_vertices_list.Add(minedge.FirstVertex);
                }
                else used_vertices_list.Add(minedge.SecondVertex);
                
            }
            return answer;
        }       
    }
}