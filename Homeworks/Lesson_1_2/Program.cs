using System;

namespace Lesson_1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            На больших объемах данных единичными операциями O(1) можно пренебречь,
            что значит, что определяющими будут циклы A, B, C. Вложенность циклов увеличивает
            сложность алгоритма, поэтому асимптотическая сложность будет рассчитываться, как
            произведение оценки сложности A, B, C => O(A(N) x B(N) x C(N)). В том случае, 
            что каждый из циклов проходится по массиву целиком, то можно заключить, что
            O(A(N) x B(N) x C(N)) => O(N^3)
             */
        }
        
        public static int  StrangeSum(int[] inputArray)
        {
            int sum = 0; // O(1)
            for (int i = 0; i < inputArray.Length; i++) // O(N) : A
            {
                for (int j = 0; j < inputArray.Length; j++) // O(N) : B
                {
                    for (int k = 0; k < inputArray.Length; k++) // O(N) : C
                    {
                        int y = 0; // O(1)

                        if (j != 0) // O(1)
                        {
                            y = k / j; // O(1)
                        }

                        sum += inputArray[i] + i + k + j + y; // O(1)
                    }
                }
            }
            return sum; // O(1)
        }
    }
}