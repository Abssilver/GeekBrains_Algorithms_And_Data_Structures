using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson_8_1
{
    public class BucketsortService
    {
        static void Main(string[] args)
        {
            int[] arrayToTest = {int.MinValue, int.MaxValue, 0, 88, 23, -10, 15, 999, -16, 888, 10};
            BucketsortService bucketsortService = new BucketsortService();
            bucketsortService.PrintArray(arrayToTest);
            bucketsortService.SortArray(arrayToTest, 5);
            bucketsortService.PrintArray(arrayToTest);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        public void SortArray(int[] arrayToSort, int numberOfBuckets = 10)
        {
            var negativeNumbers = new List<int>();
            var positiveNumbers = new List<int>();

            for (int i = 0; i < arrayToSort.Length; i++)
            {
                if (arrayToSort[i] < 0)
                {
                    if (arrayToSort[i] == int.MinValue)
                        negativeNumbers.Add(int.MaxValue);
                    else
                        negativeNumbers.Add(-1 * arrayToSort[i]);
                }
                else
                    positiveNumbers.Add(arrayToSort[i]);
            }
            BucketSort(negativeNumbers, numberOfBuckets);
            BucketSort(positiveNumbers, numberOfBuckets);
            MergeBuckets(negativeNumbers, positiveNumbers, arrayToSort);
        }

        private void MergeBuckets(List<int> negativeNumbers, List<int> positiveNumbers, int[] ArrayToMergeIn)
        {
            if (ArrayToMergeIn is null || ArrayToMergeIn.Length != negativeNumbers.Count + positiveNumbers.Count)
                throw new ArgumentException("Invalid arguments! Buckets cannot be merged!");
            
            var index = 0;
            negativeNumbers.ForEach(element =>
            {
                if (element == int.MaxValue)
                    ArrayToMergeIn[negativeNumbers.Count - ++index] = int.MinValue;
                else
                    ArrayToMergeIn[negativeNumbers.Count - ++index] = -1 * element;
            });
            positiveNumbers.ForEach(element => ArrayToMergeIn[index++] = element);
        }

        private void BucketSort(List<int> data, int numberOfBuckets)
        {
            if (data.Count < 1)
                return;
            if (data.Count < numberOfBuckets || numberOfBuckets < 1)
                numberOfBuckets = data.Count;
            
            int min = data[0];
            int max = data[0];

            data.ForEach(element =>
            {
                min = element > min ? min : element;
                max = element > max ? element : max;
            });
            
            List<int>[] buckets = new List<int>[numberOfBuckets];

            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<int>();
            }
            
            int bucketRangeCapacity = (max - min) / numberOfBuckets;
            if (bucketRangeCapacity == 0) // what means that there is an array of equal elements
                bucketRangeCapacity = 1;
            
            for (int i = 0; i < data.Count; i++)
            {
                int bucketIndex = (data[i] - min) / bucketRangeCapacity;
                bucketIndex = bucketIndex == numberOfBuckets ? bucketIndex - 1 : bucketIndex;
                buckets[bucketIndex].Add(data[i]);
            }

            Parallel.For(0, numberOfBuckets, bucket => buckets[bucket].Sort());

            var index = 0;
            for (var i = 0; i < numberOfBuckets; i++)
            for (var j = 0; j < buckets[i].Count; j++)
                data[index++] = buckets[i][j];
        }

        private void CountingSort(List<int> listToSort, int min, int max)
        {
            int[] entries = new int[max - min + 1];
  
            listToSort.ForEach(element => entries[element - min]++);
            
            int index = 0;
            for (int i = 0; i < entries.Length; i++)
            {
                while (entries[i] > 0)
                {
                    listToSort[index++] = min + i;
                    entries[i]--;
                }
            }
        }

        private void PrintArray(int[] array)
        {
            Console.WriteLine("Printing array:");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
        }
    }
}