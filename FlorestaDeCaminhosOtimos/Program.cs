using System;

class Graph
{
    private int V; // Número de vértices
    private int[,] adjacencyMatrix; // Matriz de adjacência para armazenar os pesos das arestas

    public Graph(int vertices)
    {
        V = vertices;
        adjacencyMatrix = new int[V, V];

        // Inicializa a matriz de adjacência com infinito (ausência de aresta)
        for (int i = 0; i < V; ++i)
        {
            for (int j = 0; j < V; ++j)
            {
                adjacencyMatrix[i, j] = int.MaxValue;
            }
        }

        // Inicializa a diagonal com 0 (distância de um vértice para ele mesmo)
        for (int i = 0; i < V; ++i)
        {
            adjacencyMatrix[i, i] = 0;
        }
    }

    public void AddEdge(int source, int destination, int weight)
    {
        adjacencyMatrix[source, destination] = weight;
    }

    public int[,] GetAdjacencyMatrix()
    {
        return adjacencyMatrix;
    }
}

class OptimalPathForest
{
    private Graph graph;

    public OptimalPathForest(Graph g)
    {
        graph = g;
    }

    public void RunFloydWarshall()
    {
        Console.WriteLine("Iniciando Algoritmo de Floyd-Warshall para Floresta de Caminhos Ótimos...");

        int[,] distance = (int[,])graph.GetAdjacencyMatrix().Clone();
        int V = distance.GetLength(0);

        // Algoritmo de Floyd-Warshall
        for (int k = 0; k < V; k++)
        {
            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < V; j++)
                {
                    if (distance[i, k] != int.MaxValue && distance[k, j] != int.MaxValue &&
                        distance[i, k] + distance[k, j] < distance[i, j])
                    {
                        distance[i, j] = distance[i, k] + distance[k, j];
                    }
                }
            }

            // Exibe a matriz de distâncias após cada iteração
            PrintDistanceMatrix(distance, k + 1);
        }

        // Exibe a Floresta de Caminhos Ótimos
        PrintOptimalPaths(distance);
    }

    private void PrintDistanceMatrix(int[,] distance, int iteration)
    {
        Console.WriteLine($"Matriz de Distâncias após a Iteração {iteration}:");

        int V = distance.GetLength(0);

        for (int i = 0; i < V; i++)
        {
            for (int j = 0; j < V; j++)
            {
                if (distance[i, j] == int.MaxValue)
                {
                    Console.Write("INF".PadLeft(5));
                }
                else
                {
                    Console.Write(distance[i, j].ToString().PadLeft(5));
                }
            }
            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private void PrintOptimalPaths(int[,] distance)
    {
        Console.WriteLine("Floresta de Caminhos Ótimos:");

        int V = distance.GetLength(0);

        for (int i = 0; i < V; i++)
        {
            for (int j = 0; j < V; j++)
            {
                if (i != j && distance[i, j] != int.MaxValue)
                {
                    Console.WriteLine($"Caminho ótimo de {i} para {j}: {GetOptimalPath(i, j, distance)}");
                }
            }
        }
    }

    private string GetOptimalPath(int source, int destination, int[,] distance)
    {
        if (distance[source, destination] == int.MaxValue)
        {
            return "Não há caminho ótimo.";
        }

        string optimalPath = $"{source} -> ";
        while (source != destination)
        {
            int nextVertex = -1;
            int minDistance = int.MaxValue;

            for (int k = 0; k < distance.GetLength(0); k++)
            {
                if (distance[source, k] != int.MaxValue && distance[k, destination] != int.MaxValue &&
                    distance[source, k] + distance[k, destination] < minDistance)
                {
                    nextVertex = k;
                    minDistance = distance[source, k] + distance[k, destination];
                }
            }

            optimalPath += $"{nextVertex} -> ";
            source = nextVertex;
        }

        return optimalPath.TrimEnd('-', '>', ' ');
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

        OptimalPathForest sparseOptimalPathForest = new OptimalPathForest(sparseGraph);
        sparseOptimalPathForest.RunFloydWarshall();

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

        OptimalPathForest denseOptimalPathForest = new OptimalPathForest(denseGraph);
        denseOptimalPathForest.RunFloydWarshall();
    }
}

// Grafo Esparsamente Conectado (Sparse Graph):
// Número de vértices: 10
// Arestas:
// (0, 1) com peso 3
// (0, 2) com peso 5
// (1, 3) com peso 2
// (2, 4) com peso 1
// (3, 5) com peso 4
// (4, 6) com peso 2
// (5, 7) com peso 1
// (6, 8) com peso 3
// (7, 9) com peso 2
// (8, 9) com peso 4
// (9, 0) com peso 1

// Grafo Densamente Conectado (Dense Graph):
// Número de vértices: 10
// Arestas:
// (0, 1) com peso 2
// (0, 2) com peso 4
// (0, 3) com peso 1
// (1, 4) com peso 7
// (1, 5) com peso 3
// (2, 6) com peso 5
// (2, 7) com peso 2
// (3, 8) com peso 4
// (3, 9) com peso 3
// (4, 0) com peso 2
// (5, 1) com peso 3
// (6, 2) com peso 1
// (7, 3) com peso 5
// (8, 4) com peso 2
// (9, 5) com peso 4
// (4, 6) com peso 1
// (5, 7) com peso 2
// (6, 8) com peso 3
// (7, 9) com peso 4