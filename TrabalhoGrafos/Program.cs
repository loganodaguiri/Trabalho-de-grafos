using System;
using System.Collections.Generic;

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

class DFS
{
    private Graph graph;
    private bool[] visited;
    private List<int> order; // Lista para armazenar a ordem de visitação

    public DFS(Graph g)
    {
        graph = g;
        visited = new bool[g.GetAdjList().Length];
        order = new List<int>();
    }

    public void DepthFirstSearch(int startVertex)
    {
        Console.WriteLine($"Iniciando busca em profundidade a partir do vértice {startVertex}...");

        DFSUtil(startVertex);

        Console.WriteLine("Busca em profundidade concluída.");

        Console.Write("Ordem de visita: ");
        foreach (var vertex in order)
        {
            Console.Write($"{vertex} ");
        }
        Console.WriteLine();
    }

    private void DFSUtil(int vertex)
    {
        visited[vertex] = true;
        Console.WriteLine($"Visitando vértice {vertex}");
        order.Add(vertex); // Adiciona o vértice à ordem de visita

        foreach (int neighbor in graph.GetAdjList()[vertex])
        {
            if (!visited[neighbor])
            {
                Console.WriteLine($"Explorando aresta de {vertex} para {neighbor}");
                DFSUtil(neighbor);
            }
        }
    }
}

class Program
{
    static void Main()
    {
        // Grafo Espaço Modificado
        Graph sparseGraphModified = new Graph(10);
        sparseGraphModified.AddEdge(0, 1);
        sparseGraphModified.AddEdge(0, 2);
        sparseGraphModified.AddEdge(1, 3);
        sparseGraphModified.AddEdge(2, 4);
        sparseGraphModified.AddEdge(3, 5);
        sparseGraphModified.AddEdge(4, 6);
        sparseGraphModified.AddEdge(5, 7);
        sparseGraphModified.AddEdge(6, 8);
        sparseGraphModified.AddEdge(7, 9);
        sparseGraphModified.AddEdge(8, 9);
        sparseGraphModified.AddEdge(9, 0);
        sparseGraphModified.AddEdge(0, 3);
        sparseGraphModified.AddEdge(3, 6);
        sparseGraphModified.AddEdge(6, 9);
        sparseGraphModified.AddEdge(9, 2);
        sparseGraphModified.AddEdge(2, 5);
        sparseGraphModified.AddEdge(5, 8);
        sparseGraphModified.AddEdge(8, 1);
        sparseGraphModified.AddEdge(1, 4);
        sparseGraphModified.AddEdge(4, 7);

        DFS sparseDFSModified = new DFS(sparseGraphModified);
        sparseDFSModified.DepthFirstSearch(0);


        Console.WriteLine();

        // Grafo Denso Modificado
        Graph denseGraphModified = new Graph(10);
        denseGraphModified.AddEdge(0, 1);
        denseGraphModified.AddEdge(0, 2);
        denseGraphModified.AddEdge(0, 3);
        denseGraphModified.AddEdge(1, 4);
        denseGraphModified.AddEdge(1, 5);
        denseGraphModified.AddEdge(2, 6);
        denseGraphModified.AddEdge(2, 7);
        denseGraphModified.AddEdge(3, 8);
        denseGraphModified.AddEdge(3, 9);
        denseGraphModified.AddEdge(4, 0);
        denseGraphModified.AddEdge(5, 1);
        denseGraphModified.AddEdge(6, 2);
        denseGraphModified.AddEdge(7, 3);
        denseGraphModified.AddEdge(8, 4);
        denseGraphModified.AddEdge(9, 5);
        denseGraphModified.AddEdge(4, 6);
        denseGraphModified.AddEdge(5, 7);
        denseGraphModified.AddEdge(6, 8);
        denseGraphModified.AddEdge(7, 9);
        denseGraphModified.AddEdge(0, 4);
        denseGraphModified.AddEdge(1, 6);
        denseGraphModified.AddEdge(2, 8);
        denseGraphModified.AddEdge(3, 0);
        denseGraphModified.AddEdge(4, 2);
        denseGraphModified.AddEdge(5, 9);
        denseGraphModified.AddEdge(6, 1);
        denseGraphModified.AddEdge(7, 4);
        denseGraphModified.AddEdge(8, 7);
        denseGraphModified.AddEdge(9, 3);
        denseGraphModified.AddEdge(0, 5);
        denseGraphModified.AddEdge(1, 8);
        denseGraphModified.AddEdge(2, 9);
        denseGraphModified.AddEdge(3, 1);
        denseGraphModified.AddEdge(4, 8);
        denseGraphModified.AddEdge(5, 0);
        denseGraphModified.AddEdge(6, 3);
        denseGraphModified.AddEdge(7, 6);
        denseGraphModified.AddEdge(8, 9);
        denseGraphModified.AddEdge(9, 2);
        denseGraphModified.AddEdge(9, 8);

        DFS denseDFSModified = new DFS(denseGraphModified);
        denseDFSModified.DepthFirstSearch(0);
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