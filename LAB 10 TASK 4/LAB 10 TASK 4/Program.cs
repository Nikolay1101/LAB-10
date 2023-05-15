using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_10_TASK_4
{
    internal class Program
    {
        static int[][] roadMatrix; // Матрица системы дорог
        static int maxDistance = 200; // Максимальное расстояние

        static void Main()
        {
            // Пример матрицы системы дорог (замените на свои данные)
            roadMatrix = new int[][] {
            new int[] { -1, 50, 100, -1, 150 },
            new int[] { 50, -1, -1, 80, -1 },
            new int[] { 100, -1, -1, -1, 70 },
            new int[] { -1, 80, -1, -1, -1 },
            new int[] { 150, -1, 70, -1, -1 }
        };

            int startCity = 0; // Начальный город (замените на свой город)

            List<int> reachableCities = GetReachableCities(startCity);

            Console.WriteLine("Города, в которые можно добраться из города {0} по суммарному пути не длиннее {1} км:", startCity, maxDistance);
            foreach (int city in reachableCities)
            {
                Console.WriteLine(city);
            }
        }

        static List<int> GetReachableCities(int startCity)
        {
            List<int> reachableCities = new List<int>();
            bool[] visited = new bool[roadMatrix.Length];

            DFS(startCity, 0, visited, reachableCities);

            return reachableCities;
        }

        static void DFS(int currentCity, int currentDistance, bool[] visited, List<int> reachableCities)
        {
            visited[currentCity] = true;

            if (currentDistance > maxDistance)
            {
                return;
            }

            reachableCities.Add(currentCity);

            for (int i = 0; i < roadMatrix.Length; i++)
            {
                if (!visited[i] && roadMatrix[currentCity][i] != -1)
                {
                    int newDistance = currentDistance + roadMatrix[currentCity][i];
                    DFS(i, newDistance, visited, reachableCities);
                }
            }
        }
    }
}
