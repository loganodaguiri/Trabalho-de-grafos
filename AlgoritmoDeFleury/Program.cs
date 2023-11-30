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

class Fleury
{
    private Graph graph;

    public Fleury(Graph g)
    {
        graph = g;
    }

    public void RunFleury()
    {
        Console.WriteLine("Iniciando algoritmo de Fleury...");

        int startVertex = FindStartVertex();

        if (startVertex == -1)
        {
            Console.WriteLine("O grafo não possui um ciclo euleriano.");
            return;
        }

        List<int> eulerianCycle = new List<int>();
        EulerianCycle(startVertex, eulerianCycle);

        Console.WriteLine("Ciclo Euleriano encontrado:");
        foreach (int vertex in eulerianCycle)
        {
            Console.Write(vertex + " ");
        }
    }

    private int FindStartVertex()
    {
        for (int v = 0; v < graph.GetAdjList().Length; v++)
        {
            if (graph.GetAdjList()[v].Count % 2 != 0)
            {
                return v;
            }
        }

        return -1;
    }

    private void EulerianCycle(int u, List<int> eulerianCycle)
    {
        Stack<int> stack = new Stack<int>();
        stack.Push(u);

        while (stack.Count != 0)
        {
            int currentVertex = stack.Peek();

            if (graph.GetAdjList()[currentVertex].Count > 0)
            {
                int nextVertex = graph.GetAdjList()[currentVertex][0];
                stack.Push(nextVertex);

                RemoveEdge(currentVertex, nextVertex);
            }
            else
            {
                stack.Pop();
                eulerianCycle.Add(currentVertex);
            }
        }
    }

    private void RemoveEdge(int u, int v)
    {
        graph.GetAdjList()[u].Remove(v);
        graph.GetAdjList()[v].Remove(u);
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

        Fleury sparseFleury = new Fleury(sparseGraph);
        sparseFleury.RunFleury();

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

        Fleury denseFleury = new Fleury(denseGraph);
        denseFleury.RunFleury();
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