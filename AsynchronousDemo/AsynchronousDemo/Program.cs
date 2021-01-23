using System;

namespace AsynchronousDemo
{
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            //UseChronometer();
            var arr = new int[] {12, 11, 10, 9};
            PrintArray(arr, arr.Length);
            MergeSort(arr,0, arr.Length - 1);
            PrintArray(arr, arr.Length);
        }

        private static void UseChronometer()
        {
            var chronometer = new Chronometer();
            while (true)
            {
                string command = Console.ReadLine();
                switch (command)
                {
                    case "start":
                        chronometer.Start();
                        break;
                    case "lap":
                        chronometer.Lap();
                        break;
                    case "laps":
                        chronometer.AllLaps();
                        break;
                    case "reset":
                        chronometer.Reset();
                        break;
                    case "time":
                        Console.WriteLine(chronometer.GetTime);
                        break;
                    case "stop":
                        chronometer.Stop();
                        break;
                    case "exit":
                        return;
                }
            }
        }

        private static void Merge(int[] input, int left, int middle, int right)
        {
            int[] leftArray = new int[middle - left + 1];
            int[] rightArray = new int[right - middle];

            Array.Copy(input, left, leftArray, 0, middle - left + 1);
            Array.Copy(input, middle + 1, rightArray, 0, right - middle);

            int i = 0;
            int j = 0;
            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArray.Length)
                {
                    input[k] = rightArray[j];
                    j++;
                }
                else if (j == rightArray.Length)
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else if (leftArray[i] <= rightArray[j])
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else
                {
                    input[k] = rightArray[j];
                    j++;
                }
            }
        }

        private static void MergeSort(int[] input, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                MergeSort(input, left, middle);
                MergeSort(input, middle + 1, right);

                Merge(input, left, middle, right);
            }
            else
            {
                return;
            }
        }

        private static void PrintArray(int[] array, int length)
        {
            var output = string.Join(" ", array);
            Console.WriteLine(output);
        }
    }
}
