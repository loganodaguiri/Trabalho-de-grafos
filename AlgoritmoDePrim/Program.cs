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
        adjList[w].Add((v, weight)); // Grafo não direcionado
    }

    public List<(int, int)>[] GetAdjList()
    {
        return adjList;
    }
}

class Prim
{
    private Graph graph;

    public Prim(Graph g)
    {
        graph = g;
    }

    public void RunPrim()
    {
        Console.WriteLine("Iniciando algoritmo de Prim...");

        int[] parent = new int[graph.GetAdjList().Length];
        int[] key = new int[graph.GetAdjList().Length];
        bool[] mstSet = new bool[graph.GetAdjList().Length];

        for (int i = 0; i < graph.GetAdjList().Length; ++i)
        {
            key[i] = int.MaxValue;
            mstSet[i] = false;
        }

        key[0] = 0; // Iniciar do primeiro vértice

        for (int count = 0; count < graph.GetAdjList().Length - 1; ++count)
        {
            int u = MinKey(key, mstSet);
            mstSet[u] = true;

            Console.WriteLine($"Adicionando aresta {parent[u]} - {u} com peso {key[u]} à MST");

            foreach (var neighbor in graph.GetAdjList()[u])
            {
                int v = neighbor.Item1;
                int weight = neighbor.Item2;

                if (!mstSet[v] && weight < key[v])
                {
                    parent[v] = u;
                    key[v] = weight;
                }
            }
        }

        Console.WriteLine("Algoritmo de Prim concluído.");
        PrintMST(parent, key);
    }

    private int MinKey(int[] key, bool[] mstSet)
    {
        int min = int.MaxValue, minIndex = -1;

        for (int v = 0; v < graph.GetAdjList().Length; ++v)
        {
            if (!mstSet[v] && key[v] < min)
            {
                min = key[v];
                minIndex = v;
            }
        }

        return minIndex;
    }

    private void PrintMST(int[] parent, int[] key)
    {
        Console.WriteLine("Arestas da Árvore Geradora Mínima (MST):");
        for (int i = 1; i < graph.GetAdjList().Length; ++i)
        {
            Console.WriteLine($"Aresta {parent[i]} - {i} com peso {key[i]}");
        }
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

        Prim sparsePrim = new Prim(sparseGraph);
        sparsePrim.RunPrim();

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

        Prim densePrim = new Prim(denseGraph);
        densePrim.RunPrim();
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