using System;
using System.Collections.Generic;

class Graph
{
    private int V; // Número de vértices
    private List<(int, int)>[] adjList; // Lista de adjacência (destino, peso)

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

class Dijkstra
{
    private Graph graph;

    public Dijkstra(Graph g)
    {
        graph = g;
    }

    public void RunDijkstra(int startVertex)
    {
        Console.WriteLine($"Iniciando algoritmo de Dijkstra a partir do vértice {startVertex}...");

        int[] distances = new int[graph.GetAdjList().Length];
        for (int i = 0; i < distances.Length; i++)
        {
            distances[i] = int.MaxValue;
        }

        distances[startVertex] = 0;

        PriorityQueue<(int, int)> priorityQueue = new PriorityQueue<(int, int)>((x, y) => x.Item2.CompareTo(y.Item2));
        priorityQueue.Enqueue((startVertex, 0));

        while (priorityQueue.Count != 0)
        {
            var currentVertex = priorityQueue.Dequeue();
            int u = currentVertex.Item1;
            int distanceU = currentVertex.Item2;

            Console.WriteLine($"Explorando vértice {u} com distância {distanceU}");

            foreach (var neighbor in graph.GetAdjList()[u])
            {
                int v = neighbor.Item1;
                int weightUV = neighbor.Item2;

                int distanceThroughU = distanceU + weightUV;

                if (distanceThroughU < distances[v])
                {
                    distances[v] = distanceThroughU;
                    priorityQueue.Enqueue((v, distanceThroughU));
                }
            }
        }

        Console.WriteLine("Algoritmo de Dijkstra concluído.");
        PrintDistances(distances);
    }

    private void PrintDistances(int[] distances)
    {
        Console.WriteLine("Distâncias mínimas a partir do vértice de origem:");
        for (int i = 0; i < distances.Length; i++)
        {
            Console.WriteLine($"Vértice {i}: {distances[i]}");
        }
    }
}

class PriorityQueue<T>
{
    private List<T> heap;
    private Comparison<T> comparison;

    public PriorityQueue(Comparison<T> comparison)
    {
        this.heap = new List<T>();
        this.comparison = comparison;
    }

    public int Count
    {
        get { return heap.Count; }
    }

    public void Enqueue(T item)
    {
        heap.Add(item);
        int i = heap.Count - 1;
        while (i > 0)
        {
            int j = (i - 1) / 2;
            if (comparison(heap[i], heap[j]) >= 0)
                break;
            Swap(i, j);
            i = j;
        }
    }

    public T Dequeue()
    {
        int count = heap.Count - 1;
        T result = heap[0];
        heap[0] = heap[count];
        heap.RemoveAt(count);

        count--;
        int i = 0;
        while (true)
        {
            int left = i * 2 + 1;
            if (left > count)
                break;
            int right = left + 1;
            if (right <= count && comparison(heap[right], heap[left]) < 0)
                left = right;
            if (comparison(heap[left], heap[i]) >= 0)
                break;
            Swap(i, left);
            i = left;
        }

        return result;
    }

    private void Swap(int i, int j)
    {
        T temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
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

        Dijkstra sparseDijkstra = new Dijkstra(sparseGraph);
        sparseDijkstra.RunDijkstra(0);

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

        Dijkstra denseDijkstra = new Dijkstra(denseGraph);
        denseDijkstra.RunDijkstra(0);
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