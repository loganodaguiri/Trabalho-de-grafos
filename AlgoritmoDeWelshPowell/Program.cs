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
        adjList[w].Add(v); // Grafo não direcionado
    }

    public List<int>[] GetAdjList()
    {
        return adjList;
    }
}

class WelshPowell
{
    private Graph graph;

    public WelshPowell(Graph g)
    {
        graph = g;
    }

    public void RunWelshPowell()
    {
        Console.WriteLine("Iniciando Algoritmo de Welsh-Powell...");

        // Obtém a ordem decrescente dos graus dos vértices
        List<int> sortedVertices = GetSortedVertices();

        // Inicializa o vetor de cores
        int[] colors = new int[graph.GetAdjList().Length];

        // Inicializa as cores dos vértices
        int currentColor = 1;

        foreach (int vertex in sortedVertices)
        {
            // Se o vértice ainda não possui uma cor
            if (colors[vertex] == 0)
            {
                // Atribui a cor ao vértice
                colors[vertex] = currentColor;

                // Colore os vértices não adjacentes com cores diferentes
                foreach (int adjacentVertex in graph.GetAdjList()[vertex])
                {
                    if (colors[adjacentVertex] == 0)
                    {
                        colors[adjacentVertex] = currentColor;
                    }
                }

                currentColor++;
            }
        }

        // Imprime as cores atribuídas aos vértices
        PrintColors(colors);
    }

    private List<int> GetSortedVertices()
    {
        // Calcula os graus dos vértices
        Dictionary<int, int> degrees = new Dictionary<int, int>();

        for (int i = 0; i < graph.GetAdjList().Length; ++i)
        {
            degrees[i] = graph.GetAdjList()[i].Count;
        }

        // Ordena os vértices em ordem decrescente de grau
        List<int> sortedVertices = degrees.OrderByDescending(x => x.Value)
                                          .Select(x => x.Key)
                                          .ToList();

        return sortedVertices;
    }

    private void PrintColors(int[] colors)
    {
        Console.WriteLine("Cores atribuídas aos vértices:");

        for (int i = 0; i < colors.Length; ++i)
        {
            Console.WriteLine($"Vértice {i}: Cor = {colors[i]}");
        }
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

        WelshPowell sparseWelshPowell = new WelshPowell(sparseGraph);
        sparseWelshPowell.RunWelshPowell();

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

        WelshPowell denseWelshPowell = new WelshPowell(denseGraph);
        denseWelshPowell.RunWelshPowell();
    }
}

// Grafo Esparso:
// Número de Vértices (|V|): 10
// Arestas (0-indexadas):
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
// Arestas (0-indexadas):
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