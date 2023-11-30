using System;

class FloydWarshallAlgorithm
{
    private int V; // Número de vértices
    private int[,] adjacencyMatrix; // Matriz de adjacência para armazenar os pesos das arestas

    public FloydWarshallAlgorithm(int vertices, int[,] graph)
    {
        V = vertices;
        adjacencyMatrix = (int[,])graph.Clone();
    }

    public void RunFloydWarshall()
    {
        Console.WriteLine("Iniciando Algoritmo de Floyd-Warshall...");

        // Exibe a matriz de adjacência inicial
        Console.WriteLine("Matriz de Adjacência Inicial:");
        PrintAdjacencyMatrix(adjacencyMatrix);

        // Algoritmo de Floyd-Warshall
        for (int k = 0; k < V; k++)
        {
            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < V; j++)
                {
                    if (adjacencyMatrix[i, k] != int.MaxValue && adjacencyMatrix[k, j] != int.MaxValue &&
                        adjacencyMatrix[i, k] + adjacencyMatrix[k, j] < adjacencyMatrix[i, j])
                    {
                        adjacencyMatrix[i, j] = adjacencyMatrix[i, k] + adjacencyMatrix[k, j];
                    }
                }
            }

            // Exibe a matriz de distâncias após cada iteração
            Console.WriteLine($"\nMatriz de Distâncias após a Iteração {k + 1}:");
            PrintAdjacencyMatrix(adjacencyMatrix);
        }
    }

    private void PrintAdjacencyMatrix(int[,] matrix)
    {
        int V = matrix.GetLength(0);

        for (int i = 0; i < V; i++)
        {
            for (int j = 0; j < V; j++)
            {
                if (matrix[i, j] == int.MaxValue)
                {
                    Console.Write("INF".PadLeft(5));
                }
                else
                {
                    Console.Write(matrix[i, j].ToString().PadLeft(5));
                }
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

        FloydWarshallAlgorithm sparseFloydWarshall = new FloydWarshallAlgorithm(10, sparseGraph);
        sparseFloydWarshall.RunFloydWarshall();

        Console.WriteLine();

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

        FloydWarshallAlgorithm denseFloydWarshall = new FloydWarshallAlgorithm(10, denseGraph);
        denseFloydWarshall.RunFloydWarshall();
    }
}
