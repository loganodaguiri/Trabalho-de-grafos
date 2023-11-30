using System;
using System.Collections.Generic;
using System.Linq;

class Graph
{
    private int V; // Número de vértices
    private List<int>[] adjList; // Lista de adjacência

    public Graph(int vertices)
    {
        V = vertices;
        adjList = new List<int>[V];
        for (int i = 0; i < V; ++i)
        {
            adjList[i] = new List<int>();
        }
    }

    public void AddEdge(int v, int w)
    {
        adjList[v].Add(w);
    }

    public List<int>[] GetAdjList()
    {
        return adjList;
    }
}

class BFS
{
    private Graph graph;

    public BFS(Graph g)
    {
        graph = g;
    }

    public void BreadthFirstSearch(int startVertex)
    {
        Console.WriteLine($"Iniciando busca em largura a partir do vértice {startVertex}...");

        bool[] visited = new bool[graph.GetAdjList().Length];
        Queue<int> queue = new Queue<int>();

        visited[startVertex] = true;
        queue.Enqueue(startVertex);

        while (queue.Count != 0)
        {
            int currentVertex = queue.Dequeue();
            Console.WriteLine($"Visitando vértice {currentVertex}");

            foreach (int neighbor in graph.GetAdjList()[currentVertex])
            {
                if (!visited[neighbor])
                {
                    Console.WriteLine($"Explorando aresta de {currentVertex} para {neighbor}");
                    visited[neighbor] = true;
                    queue.Enqueue(neighbor);
                }
            }
        }

        Console.WriteLine("Busca em largura concluída.");
    }
}

class Program
{
    static void Main()
    {
        // Grafo Esparso
        Graph sparseGraph = new Graph(10);
        sparseGraph.AddEdge(0, 1);
        sparseGraph.AddEdge(0, 2);
        sparseGraph.AddEdge(1, 3);
        sparseGraph.AddEdge(2, 4);
        sparseGraph.AddEdge(3, 5);
        sparseGraph.AddEdge(4, 6);
        sparseGraph.AddEdge(5, 7);
        sparseGraph.AddEdge(6, 8);
        sparseGraph.AddEdge(7, 9);
        sparseGraph.AddEdge(8, 9);
        sparseGraph.AddEdge(9, 0);

        BFS sparseBFS = new BFS(sparseGraph);
        sparseBFS.BreadthFirstSearch(0);

        Console.WriteLine();

        // Grafo Denso
        Graph denseGraph = new Graph(10);
        denseGraph.AddEdge(0, 1);
        denseGraph.AddEdge(0, 2);
        denseGraph.AddEdge(0, 3);
        denseGraph.AddEdge(1, 4);
        denseGraph.AddEdge(1, 5);
        denseGraph.AddEdge(2, 6);
        denseGraph.AddEdge(2, 7);
        denseGraph.AddEdge(3, 8);
        denseGraph.AddEdge(3, 9);
        denseGraph.AddEdge(4, 0);
        denseGraph.AddEdge(5, 1);
        denseGraph.AddEdge(6, 2);
        denseGraph.AddEdge(7, 3);
        denseGraph.AddEdge(8, 4);
        denseGraph.AddEdge(9, 5);
        denseGraph.AddEdge(4, 6);
        denseGraph.AddEdge(5, 7);
        denseGraph.AddEdge(6, 8);
        denseGraph.AddEdge(7, 9);

        BFS denseBFS = new BFS(denseGraph);
        denseBFS.BreadthFirstSearch(0);
    }
}


// Grafo Esparso:
// Número de Vértices (|V|): 10
// Número de Arestas (|E|): 20
// Arestas:
// 0 - 1
// 0 - 2
// 1 - 3
// 2 - 4
// 3 - 5
// 4 - 6
// 5 - 7
// 6 - 8
// 7 - 9
// 8 - 9
// 9 - 0

// Grafo Denso:
// Número de Vértices (|V|): 10
// Número de Arestas (|E|): 40
// Arestas:
// 0 - 1
// 0 - 2
// 0 - 3
// 1 - 4
// 1 - 5
// 2 - 6
// 2 - 7
// 3 - 8
// 3 - 9
// 4 - 0
// 5 - 1
// 6 - 2
// 7 - 3
// 8 - 4
// 9 - 5
// 4 - 6
// 5 - 7
// 6 - 8
// 7 - 9