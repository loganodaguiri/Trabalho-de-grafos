using System;
using System.Collections.Generic;

class Graph
{
    private int V; // Número de vértices
    private List<(int, int)>[] adjList; // Lista de adjacência (vizinho, peso)

    public Graph(int vertices)
    {
        V = vertices;
        adjList = new List<(int, int)>[V];
        for (int i = 0; i < V; ++i)
        {
            adjList[i] = new List<(int, int)>();
        }
    }

    public void AddEdge(int v, int w, int weight)
    {
        adjList[v].Add((w, weight));
    }

    public List<(int, int)>[] GetAdjList()
    {
        return adjList;
    }
}

class BellmanFord
{
    private Graph graph;

    public BellmanFord(Graph g)
    {
        graph = g;
    }

    public void RunBellmanFord(int source)
    {
        int[] distance = new int[graph.GetAdjList().Length];
        int[] predecessor = new int[graph.GetAdjList().Length];

        // Inicialização
        for (int i = 0; i < graph.GetAdjList().Length; ++i)
        {
            distance[i] = int.MaxValue;
            predecessor[i] = -1;
        }

        distance[source] = 0;

        Console.WriteLine($"Iniciando Algoritmo de Bellman-Ford a partir do vértice {source}...");

        // Relaxamento de arestas repetidas |V| - 1 vezes
        for (int i = 0; i < graph.GetAdjList().Length - 1; ++i)
        {
            foreach (int u in Enumerable.Range(0, graph.GetAdjList().Length))
            {
                foreach (var edge in graph.GetAdjList()[u])
                {
                    int v = edge.Item1;
                    int weight = edge.Item2;

                    if (distance[u] != int.MaxValue && distance[u] + weight < distance[v])
                    {
                        distance[v] = distance[u] + weight;
                        predecessor[v] = u;
                    }
                }
            }

            PrintIterationResults(i + 1, distance, predecessor);
        }

        // Verifica ciclo de peso negativo
        if (HasNegativeCycle(distance))
        {
            Console.WriteLine("O grafo contém um ciclo de peso negativo.");
        }
        else
        {
            Console.WriteLine("Algoritmo de Bellman-Ford concluído.");
            PrintResults(distance, predecessor);
        }
    }

    private void PrintIterationResults(int iteration, int[] distance, int[] predecessor)
    {
        Console.WriteLine($"Iteração {iteration}:");

        for (int i = 0; i < distance.Length; ++i)
        {
            Console.WriteLine($"Vértice {i}: Distância = {distance[i]}, Antecessor = {predecessor[i]}");
        }

        Console.WriteLine();
    }

    private void PrintResults(int[] distance, int[] predecessor)
    {
        Console.WriteLine("Resultado Final:");

        for (int i = 0; i < distance.Length; ++i)
        {
            Console.WriteLine($"Vértice {i}: Distância = {distance[i]}, Antecessor = {predecessor[i]}");
        }
    }

    private bool HasNegativeCycle(int[] distance)
    {
        foreach (int u in Enumerable.Range(0, graph.GetAdjList().Length))
        {
            foreach (var edge in graph.GetAdjList()[u])
            {
                int v = edge.Item1;
                int weight = edge.Item2;

                if (distance[u] != int.MaxValue && distance[u] + weight < distance[v])
                {
                    return true;
                }
            }
        }

        return false;
    }
}

class Program
{
    static void Main()
    {
        // Grafo Esparso
        Graph sparseGraph = new Graph(10);
        sparseGraph.AddEdge(0, 1, 3);
        sparseGraph.AddEdge(0, 2, 5);
        sparseGraph.AddEdge(1, 3, 2);
        sparseGraph.AddEdge(2, 4, 1);
        sparseGraph.AddEdge(3, 5, 4);
        sparseGraph.AddEdge(4, 6, 2);
        sparseGraph.AddEdge(5, 7, 1);
        sparseGraph.AddEdge(6, 8, 3);
        sparseGraph.AddEdge(7, 9, 2);
        sparseGraph.AddEdge(8, 9, 4);
        sparseGraph.AddEdge(9, 0, 1);

        BellmanFord sparseBellmanFord = new BellmanFord(sparseGraph);
        sparseBellmanFord.RunBellmanFord(0);

        Console.WriteLine();

        // Grafo Denso
        Graph denseGraph = new Graph(10);
        denseGraph.AddEdge(0, 1, 2);
        denseGraph.AddEdge(0, 2, 4);
        denseGraph.AddEdge(0, 3, 1);
        denseGraph.AddEdge(1, 4, 7);
        denseGraph.AddEdge(1, 5, 3);
        denseGraph.AddEdge(2, 6, 5);
        denseGraph.AddEdge(2, 7, 2);
        denseGraph.AddEdge(3, 8, 4);
        denseGraph.AddEdge(3, 9, 3);
        denseGraph.AddEdge(4, 0, 2);
        denseGraph.AddEdge(5, 1, 3);
        denseGraph.AddEdge(6, 2, 1);
        denseGraph.AddEdge(7, 3, 5);
        denseGraph.AddEdge(8, 4, 2);
        denseGraph.AddEdge(9, 5, 4);
        denseGraph.AddEdge(4, 6, 1);
        denseGraph.AddEdge(5, 7, 2);
        denseGraph.AddEdge(6, 8, 3);
        denseGraph.AddEdge(7, 9, 4);

        BellmanFord denseBellmanFord = new BellmanFord(denseGraph);
        denseBellmanFord.RunBellmanFord(0);
    }
}

// Grafo Esparso:
// Número de Vértices (|V|): 10
// Número de Arestas (|E|): 20
// Arestas com seus respectivos pesos:
// 0 - 1 (3)
// 0 - 2 (5)
// 1 - 3 (2)
// 2 - 4 (1)
// 3 - 5 (4)
// 4 - 6 (2)
// 5 - 7 (1)
// 6 - 8 (3)
// 7 - 9 (2)
// 8 - 9 (4)
// 9 - 0 (1)

// Grafo Denso:
// Número de Vértices (|V|): 10
// Número de Arestas (|E|): 40
// Arestas com seus respectivos pesos:
// 0 - 1 (2)
// 0 - 2 (4)
// 0 - 3 (1)
// 1 - 4 (7)
// 1 - 5 (3)
// 2 - 6 (5)
// 2 - 7 (2)
// 3 - 8 (4)
// 3 - 9 (3)
// 4 - 0 (2)
// 5 - 1 (3)
// 6 - 2 (1)
// 7 - 3 (5)
// 8 - 4 (2)
// 9 - 5 (4)
// 4 - 6 (1)
// 5 - 7 (2)
// 6 - 8 (3)
// 7 - 9 (4)