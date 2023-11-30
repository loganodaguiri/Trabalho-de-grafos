using System;
using System.Collections.Generic;

class FordFulkerson
{
    private int V;
    private int[,] residualGraph;

    public FordFulkerson(int vertices, int[,] graph)
    {
        V = vertices;
        residualGraph = new int[V, V];
        Array.Copy(graph, residualGraph, graph.Length);
    }

    private bool BreadthFirstSearch(int s, int t, out List<int> path)
    {
        path = new List<int>(V);
        bool[] visited = new bool[V];
        Queue<int> queue = new Queue<int>();

        visited[s] = true;
        queue.Enqueue(s);

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();

            for (int v = 0; v < V; v++)
            {
                if (!visited[v] && residualGraph[u, v] > 0)
                {
                    queue.Enqueue(v);
                    path.Add(v);
                    visited[v] = true;

                    if (v == t)
                        return true;
                }
            }
        }

        return false;
    }

    private int MinCapacity(List<int> path)
    {
        int minCapacity = int.MaxValue;

        for (int i = 0; i < path.Count - 1; i++)
        {
            int u = path[i];
            int v = path[i + 1];
            minCapacity = Math.Min(minCapacity, residualGraph[u, v]);
        }

        return minCapacity;
    }

    private void UpdateResidualGraph(List<int> path, int minCapacity)
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            int u = path[i];
            int v = path[i + 1];

            residualGraph[u, v] -= minCapacity;
            residualGraph[v, u] += minCapacity;
        }
    }

    public int MaxFlow(int source, int sink)
    {
        int maxFlow = 0;
        List<int> path;

        while (BreadthFirstSearch(source, sink, out path))
        {
            int minCapacity = MinCapacity(path);
            UpdateResidualGraph(path, minCapacity);
            maxFlow += minCapacity;

            Console.WriteLine($"Aumentando caminho: {string.Join(" -> ", path)} com capacidade mínima {minCapacity}");
            PrintResidualGraph();
        }

        return maxFlow;
    }

    private void PrintResidualGraph()
    {
        Console.WriteLine("Grafo Residual Atualizado:");
        for (int i = 0; i < V; i++)
        {
            for (int j = 0; j < V; j++)
            {
                Console.Write(residualGraph[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        // Grafo Esparso
        int[,] sparseGraph = new int[,]
        {
            {0, 3, 5, 0, 0, 0, 0, 0, 0, 1},
            {0, 0, 0, 2, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 4, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 2, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 3, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 2},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        // Grafo Denso
        int[,] denseGraph = new int[,]
        {
            {0, 2, 4, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 7, 3, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 5, 2, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 4, 3},
            {2, 0, 0, 0, 0, 0, 1, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 3, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        FordFulkerson sparseFordFulkerson = new FordFulkerson(10, sparseGraph);
        Console.WriteLine($"Fluxo Máximo (Grafo Esparso): {sparseFordFulkerson.MaxFlow(0, 9)}");

        FordFulkerson denseFordFulkerson = new FordFulkerson(10, denseGraph);
        Console.WriteLine($"Fluxo Máximo (Grafo Denso): {denseFordFulkerson.MaxFlow(0, 9)}");
    }
}
