using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_10_TASK_2
{
    class CountingSortExample
    {
        static void Main()
        {
            int[] bills = GenerateRandomBills(100);

            Console.WriteLine("Исходный массив купюр:");
            PrintArray(bills);

            CountingSort(bills);

            Console.WriteLine("\nОтсортированный массив купюр:");
            PrintArray(bills);
        }

        static int[] GenerateRandomBills(int length)
        {
            int[] bills = new int[length];
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int randomBillIndex = random.Next(0, 7);
                int[] billDenominations = { 1, 2, 5, 10, 20, 50, 100 };
                bills[i] = billDenominations[randomBillIndex];
            }

            return bills;
        }

        static void CountingSort(int[] array)
        {
            int maxValue = GetMaxValue(array);

            int[] countingArray = new int[maxValue + 1];
            int[] sortedArray = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                countingArray[array[i]]++;
            }

            for (int i = 1; i < countingArray.Length; i++)
            {
                countingArray[i] += countingArray[i - 1];
            }

            for (int i = array.Length - 1; i >= 0; i--)
            {
                sortedArray[countingArray[array[i]] - 1] = array[i];
                countingArray[array[i]]--;
            }

            Array.Copy(sortedArray, array, array.Length);
        }

        static int GetMaxValue(int[] array)
        {
            int maxValue = int.MinValue;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > maxValue)
                {
                    maxValue = array[i];
                }
            }

            return maxValue;
        }

        static void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }

            Console.WriteLine();
        }
    }
}
