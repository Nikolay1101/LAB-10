using System;
using System.Diagnostics;
using System.IO;

class SortingAlgorithms
{
    static void Main()
    {
        int[] randomArray = GenerateRandomArray(100000);
        int[] ascendingArray = GenerateAscendingArray(100000);
        int[] descendingArray = GenerateDescendingArray(100000);

        // Сортировка слиянием
        Console.WriteLine("Сортировка слиянием:");
        RunSortingAlgorithm(MergeSort, randomArray, "random");
        RunSortingAlgorithm(MergeSort, ascendingArray, "ascending");
        RunSortingAlgorithm(MergeSort, descendingArray, "descending");
        Console.WriteLine();

        // Сортировка пирамидальная (Heapsort)
        Console.WriteLine("Сортировка пирамидальная:");
        RunSortingAlgorithm(HeapSort, randomArray, "random");
        RunSortingAlgorithm(HeapSort, ascendingArray, "ascending");
        RunSortingAlgorithm(HeapSort, descendingArray, "descending");
        Console.WriteLine();

        // Быстрая сортировка (Quicksort)
        Console.WriteLine("Быстрая сортировка:");
        RunSortingAlgorithm(QuickSort, randomArray, "random");
        RunSortingAlgorithm(QuickSort, ascendingArray, "ascending");
        RunSortingAlgorithm(QuickSort, descendingArray, "descending");
        Console.WriteLine();

        Console.WriteLine("Сортировка завершена.");
    }

    static void RunSortingAlgorithm(Action<int[]> sortFunction, int[] array, string caseName)
    {
        int[] arrayCopy = (int[])array.Clone();

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        sortFunction(arrayCopy);

        stopwatch.Stop();

        string fileName = "sorted_" + caseName + ".dat";
        WriteSortedArrayToFile(arrayCopy, fileName);

        Console.WriteLine("Время выполнения ({0}): {1} секунд : {2} миллисекунд", caseName, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds);
        Console.WriteLine("Количество сравнений: {0}", Comparisons);
        Console.WriteLine("Количество перестановок: {0}", Swaps);
        Console.WriteLine("Проверка сортировки: {0}", IsSorted(arrayCopy) ? "Пройдена" : "Не пройдена");
        Console.WriteLine();
    }

    static int[] GenerateRandomArray(int length)
    {
        Random random = new Random();
        int[] array = new int[length];

        for (int i = 0; i < length; i++)
        {
            array[i] = random.Next();
        }

        return array;
    }

    static int[] GenerateAscendingArray(int length)
    {
        int[] array = new int[length];

        for (int i = 0; i < length; i++)
        {
            array[i] = i;
        }

        return array;
    }

    static int[] GenerateDescendingArray(int length)
    {
        int[] array = new int[length];

        for (int i = 0; i < length; i++)
        {
            array[i] = length - i;
        }

        return array;
    }

    static void WriteSortedArrayToFile(int[] array, string fileName)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
        {
            foreach (int num in array)
            {
                writer.Write(num);
            }
        }
    }
    static bool IsSorted(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            if (array[i] > array[i + 1])
                return false;
        }

        return true;
    }

    static int Comparisons;
    static int Swaps;

    // Алгоритм сортировки слиянием
    static void MergeSort(int[] array)
    {
        Comparisons = 0;
        Swaps = 0;

        MergeSortRecursive(array, 0, array.Length - 1);
    }

    static void MergeSortRecursive(int[] array, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;
            MergeSortRecursive(array, left, middle);
            MergeSortRecursive(array, middle + 1, right);
            Merge(array, left, middle, right);
        }
    }

    static void Merge(int[] array, int left, int middle, int right)
    {
        int[] temp = new int[right - left + 1];

        int i = left;
        int j = middle + 1;
        int k = 0;

        while (i <= middle && j <= right)
        {
            Comparisons++;
            if (array[i] <= array[j])
            {
                temp[k] = array[i];
                i++;
            }
            else
            {
                temp[k] = array[j];
                j++;
            }
            k++;
        }

        while (i <= middle)
        {
            temp[k] = array[i];
            i++;
            k++;
        }

        while (j <= right)
        {
            temp[k] = array[j];
            j++;
            k++;
        }

        for (int m = 0; m < temp.Length; m++)
        {
            array[left + m] = temp[m];
            Swaps++;
        }
    }

    // Алгоритм пирамидальной сортировки
    static void HeapSort(int[] array)
    {
        Comparisons = 0;
        Swaps = 0;

        int length = array.Length;

        for (int i = length / 2 - 1; i >= 0; i--)
        {
            Heapify(array, length, i);
        }

        for (int i = length - 1; i > 0; i--)
        {
            Swap(array, 0, i);
            Swaps++;
            Heapify(array, i, 0);
        }
    }

    static void Heapify(int[] array, int length, int rootIndex)
    {
        int largestIndex = rootIndex;
        int leftChildIndex = 2 * rootIndex + 1;
        int rightChildIndex = 2 * rootIndex + 2;

        Comparisons++;
        if (leftChildIndex < length && array[leftChildIndex] > array[largestIndex])
        {
            largestIndex = leftChildIndex;
        }

        Comparisons++;
        if (rightChildIndex < length && array[rightChildIndex] > array[largestIndex])
        {
            largestIndex = rightChildIndex;
        }

        if (largestIndex != rootIndex)
        {
            Swap(array, rootIndex, largestIndex);
            Swaps++;
            Heapify(array, length, largestIndex);
        }
    }

    // Алгоритм быстрой сортировки
    static void QuickSort(int[] array)
    {
        Comparisons = 0;
        Swaps = 0;

        QuickSortRecursive(array, 0, array.Length - 1);
    }
    static void QuickSortRecursive(int[] array, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(array, left, right);
            QuickSortRecursive(array, left, pivotIndex - 1);
            QuickSortRecursive(array, pivotIndex + 1, right);
        }
    }

    static int Partition(int[] array, int left, int right)
    {
        int pivot = array[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            Comparisons++;
            if (array[j] < pivot)
            {
                i++;
                Swap(array, i, j);
                Swaps++;
            }
        }

        Swap(array, i + 1, right);
        Swaps++;

        return i + 1;
    }

    static void Swap(int[] array, int index1, int index2)
    {
        int temp = array[index1];
        array[index1] = array[index2];
        array[index2] = temp;
    }
}