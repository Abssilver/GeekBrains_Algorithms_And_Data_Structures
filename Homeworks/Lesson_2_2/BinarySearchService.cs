﻿using System;

namespace Lesson_2_2
{
    public class BinarySearchService
    {
        static void Main(string[] args)
        {
            /*
            На больших объемах данных единичными операциями O(1) можно пренебречь,
            что значит, что определяющим будет цикл А. Чтобы понять, какова его асимптотическая сложность
            можно обратиться к оценочному заключению на основе входных данных в самом худшем случае.
            Для оценки выберем входные данные из 10, 100 и 1000 элементов.
            
            а) 10 элементов.
            На вход подается отсортированный массив и худший случай - это поиск краевых элементов, 
            например 10-ый элемент, для простоты и рассмотрения худшего случая будем считать, что элементы
            уникальны, их зачение соответствует их индексу и искомый элемент есть в массиве. 
            Индекс округляем по правилу приведения int.
            (0 + 10) / 2  = 5 => 
            5 < 10 => (6 + 10) / 2 = 8 =>      (1)
            8 < 10 => (9 + 10) / 2 = 9 =>      (2)
            9 < 10 => (10 + 10) / 2 = 10       (3)
            
            n = 10, количество операций 3
            
            б) 100 элементов. (логика та же)
            (0 + 100) / 2  = 50 => 
            50 < 100 => (51 + 100) / 2 = 75 =>      (1)
            75 < 100 => (76 + 100) / 2 = 88 =>      (2)
            88 < 100 => (89 + 100) / 2 = 94 =>      (3)
            94 < 100 => (95 + 100) / 2 = 97 =>      (4)
            97 < 100 => (98 + 100) / 2 = 99 =>      (5)
            99 < 100 => (100 + 100) / 2 = 100       (6)
            
            n = 100, количество операций 6
            
            в) 1000 элементов.
            (0 + 1000) / 2  = 500 => 
            500 < 1000 => (501 + 1000) / 2 = 750 =>      (1)
            750 < 1000 => (751 + 1000) / 2 = 875 =>      (2)
            875 < 1000 => (876 + 1000) / 2 = 938 =>      (3)
            938 < 1000 => (939 + 1000) / 2 = 969 =>      (4)
            969 < 1000 => (970 + 1000) / 2 = 985 =>      (5)
            985 < 1000 => (986 + 1000) / 2 = 993 =>      (6)
            993 < 1000 => (994 + 1000) / 2 = 997 =>      (7)
            998 < 1000 => (998 + 1000) / 2 = 999 =>      (8)
            999 < 1000 => (1000 + 1000) / 2 = 1000       (9)

            n = 1000, количество операций 9
            
            f(n) => a, где а - количество операций (в нашем случае 3), n - количество входных элементов
            f(n^2) => 2 * а ,
            f(n^3) => 3 * a ,
            т.е. общий случай: f(n) => log{n}n * a, перепишем
            
            f(n) => log{n}n * a => 1 * a
            f(n^2) => log{n}n^2 * a => 2 * a
            f(n^3) => log{n}n^3 * a => 3 * a
            При увеличении входных данных, количество операций меняется логарифмически.
            
            Остюда делаем вывод, что асимптотическая сложность цикла будет рассчитываться, как O(log(n)).
            А по самому первому заключению следует, что сложность всей функции O(log(n)).
            */
        }

        public int BinarySearch(int[] array, int searchValue)
        {
            if (array == null || array.Length == 0) // O(1) + O(1)
            {
                throw new ArgumentException("Input array is invalid"); // O(1)
            }
            int rightLimit = array.Length - 1; // O(1)
            int leftLimit = 0; // O(1)
            while (rightLimit >= leftLimit) // O(log(n)) : A
            {
                int middle = (leftLimit + rightLimit) / 2; // O(1)
                if (array[middle] == searchValue) // O(1)
                    return middle; // O(1)
                if (array[middle] < searchValue) // O(1)
                    leftLimit = middle + 1; // O(1)
                else
                    rightLimit = middle - 1; // O(1)
            }
            return -1; // O(1)
        }
    }
}