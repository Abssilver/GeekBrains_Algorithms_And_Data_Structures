using System;
using System.Collections.Generic;
using System.IO;
using Lesson_8_1;

namespace Lesson_8_2
{
    public class ExternalSortService
    {
        static void Main(string[] args)
        {
            ExternalSortService sortService = new ExternalSortService();
            sortService.GenerateData("test.txt", 100);
            sortService.ExternalSort("test.txt", 10, out _);
            Console.WriteLine("Sorting is completed. Press any key to continue");
            Console.ReadKey();
        }

        public void ExternalSort(string filePath, int bucketSize, out string mergedBucket)
        {
            BucketsortService sortService = new BucketsortService();
            int bucketIndex = 0;
            if (bucketSize <= 0)
                bucketSize = 1;
            if (bucketSize > 1000)
                bucketSize = 1000;
            if (File.Exists(filePath))
            {
                using StreamReader reader = new StreamReader(File.Open(filePath, FileMode.Open, FileAccess.Read));
                string line;
                int[] bucket = new int[bucketSize];
                int index = 0;
                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    if (int.TryParse(line, out var value))
                    {
                        bucket[index++] = value;
                        if (index >= bucketSize)
                        {
                            sortService.SortArray(bucket, bucketSize / 10);
                            WriteTempData($"bucket_{bucketIndex++}.txt", bucket);
                            bucket = new int[bucketSize];
                            index = 0;
                        }
                    }
                }

                if (index!=0)
                {
                    var bucketCopy = new int[index];
                    for (int i = 0; i < index; i++)
                        bucketCopy[i] = bucket[i];
                    sortService.SortArray(bucketCopy, bucketSize / 10);
                    WriteTempData($"bucket_{bucketIndex++}.txt", bucketCopy);
                }
            }

            Queue<string> mergeQueue = new Queue<string>();
            for (int i = 0; i < bucketIndex; i++)
                mergeQueue.Enqueue($"bucket_{i}.txt");

            mergedBucket = $"bucket_{bucketIndex - 1}.txt";
            while (mergeQueue.Count > 1)
            {
                var firstBucket = mergeQueue.Dequeue();
                var secondBucket = mergeQueue.Dequeue();
                mergedBucket = $"bucket_{bucketIndex++}.txt";
                MergeData(mergedBucket, firstBucket, secondBucket);
                mergeQueue.Enqueue(mergedBucket);
            }
        }

        private void MergeData(string mergeTo, string mergeFromFirst, string mergeFromSecond)
        {
            using (StreamReader reader =
                new StreamReader(File.Open($"{mergeFromFirst}", FileMode.Open, FileAccess.Read)))
            {
                using (StreamReader anotherReader =
                    new StreamReader(File.Open($"{mergeFromSecond}", FileMode.Open, FileAccess.Read)))
                {
                    string line;
                    string anotherLine;
                    using (StreamWriter writer = 
                        new StreamWriter(File.Open($"{mergeTo}", FileMode.Create, FileAccess.Write)))
                    {
                        line = reader.ReadLine();
                        anotherLine = anotherReader.ReadLine();
                        {
                            while (!string.IsNullOrEmpty(line) && !string.IsNullOrEmpty(anotherLine))
                            {
                                if (int.TryParse(line, out var value_1) &&
                                    int.TryParse(anotherLine, out var value_2))
                                {
                                    if (value_1 < value_2)
                                    {
                                        writer.WriteLine(value_1);
                                        line = reader.ReadLine();
                                    }
                                    else
                                    {
                                        writer.WriteLine(value_2);
                                        anotherLine = anotherReader.ReadLine();
                                    }
                                }
                                else
                                {
                                    throw new IOException("Invalid data in file!");
                                }
                            }

                            if (anotherLine is not null)
                            {
                                writer.WriteLine(anotherLine);
                                while (!anotherReader.EndOfStream)
                                {
                                    writer.WriteLine(anotherReader.ReadLine());
                                }
                            }
                            else if (line is not null)
                            {
                                writer.WriteLine(line);
                                while (!reader.EndOfStream)
                                {
                                    writer.WriteLine(reader.ReadLine());
                                }  
                            }
                        }
                    }
                }
            }
        }

        private void WriteTempData(string filePath, int[] data)
        {
            using StreamWriter writer =
                new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write));
            for (int i = 0; i < data.Length; i++)
                writer.WriteLine(data[i]);
        }

        private void GenerateData(string filePath, int numberOfValues)
        {
            Random rnd = new Random();

            using StreamWriter writer =
                new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write));
            for (int i = 0; i < numberOfValues; i++)
            {
                var value = rnd.Next(int.MinValue, int.MaxValue);
                writer.WriteLine(value);
            }
        }
    }
}