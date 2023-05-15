using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Graph
{
    private int V = 0;
    private int[,] graph = null;

    public Graph(int[,] adj, int N)
    {
        graph = adj;
        V = N;
    }

    public Stack<int> backChain(int[] p, int startPos, int endPos)
    {
        int pos = endPos;

        Stack<int> pathStack = new Stack<int>();
        pathStack.Push(pos);

        while (pos != startPos)
        {
            pos = p[pos];
            pathStack.Push(pos);
        }

        return pathStack;
    }

    public Stack<int> DFS(int startPos, int endPos)
    {
        Stack<int> st = new Stack<int>();

        int[] vPath = new int[V];

        int[] checkedv = new int[V];


        st.Push(startPos);
        checkedv[startPos] = 1;

        while (st.Count > 0)
        {
            int i = st.Pop();

            for (int j = V - 1; j >= 0; j--)
            {
                if (graph[i, j] > 0 && checkedv[j] == 0)
                {
                    checkedv[j] = 1;
                    st.Push(j);
                    vPath[j] = i;

                    if (j == endPos)
                    {
                        return backChain(vPath, startPos, endPos);
                    }
                }
            }
        }

        return null;
    }

    public Stack<int> BFS(int startPos, int endPos)
    {
        Queue<int> q = new Queue<int>();

        int[] vPath = new int[V];

        int[] checkedv = new int[V];

        q.Enqueue(startPos);
        checkedv[startPos] = 1;

        while (q.Count > 0)
        {
            int i = q.Dequeue();

            for (int j = 0; j < V; j++)
            {
                if (graph[i, j] > 0 && checkedv[j] == 0)
                {
                    checkedv[j] = 1;
                    q.Enqueue(j);
                    vPath[j] = i;

                    if (j == endPos)
                    {
                        return backChain(vPath, startPos, endPos);
                    }
                }
            }
        }
        return null;
    }
}


class Program
{
    static int ReadNum()
    {
        int x = 0;
        while (x < 1 || x > 8)
        {
            try { x = Convert.ToInt32(Console.ReadLine()); }
            catch { Console.Write("Введите нормальную вершину "); }

            if (x < 1 || x > 8) Console.Write("Введите нормальную вершину ");
        }
        return x;
    }

    static void Main(string[] args)
    {
        var matrix = new int[,] {
            {0, 1, 1, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 1, 1, 0},
            {1, 0, 0, 1, 0, 1, 0, 1},
            {0, 0, 1, 0, 1, 0, 0, 0},
            {0, 0, 0, 1, 0, 1, 0, 0},
            {0, 1, 1, 0, 1, 0, 0, 0},
            {0, 1, 0, 0, 0, 0, 0, 1},
            {0, 0, 1, 0, 0, 0, 1, 0}
        };
        var graph = new Graph(matrix, 8);

        Console.Write("Введите вершину X:");
        int x = ReadNum();
        Console.Write("Введите вершину Y:");
        int y = ReadNum();


        int cnt = 0;
        Console.WriteLine("BFS:");
        foreach (var num in graph.BFS(x - 1, y - 1))
        {
            if (cnt == 0) Console.Write(num + 1);
            else Console.Write("->" + (num + 1));
            cnt++;
        }
        Console.WriteLine();

        cnt = 0;
        Console.WriteLine("DFS:");
        foreach (var num in graph.DFS(x - 1, y - 1))
        {
            if (cnt == 0) Console.Write(num + 1);
            else Console.Write("->" + (num + 1));
            cnt++;
        }
        Console.WriteLine();
    }
}
