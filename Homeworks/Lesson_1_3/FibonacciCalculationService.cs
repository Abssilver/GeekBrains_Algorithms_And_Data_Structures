using System;

namespace Lesson_1_3
{
    public class FibonacciCalculationService
    {
        static void Main(string[] args)
        {
            //Проверка находится в проекте UnitTest_Lesson_1_3
        }
        public int GetFibonacciNumberRecursion(int index)
        {
            if (index < 0) throw new ArgumentException("An argument must be greater than zero!");
            if (index == 1) return 1;
            else if (index == 0) return 0;
            else return GetFibonacciNumberRecursion(--index) + GetFibonacciNumberRecursion(--index);
        }

        public int GetFibonacciNumberLoop(int index)
        {
            if (index < 0) throw new ArgumentException("An argument must be greater than zero!");
            int previous = 0;
            int pre_previous = 0;
            int buffer;
            for (int i = 1; i <= index; i++)
            {
                if (i == 1)
                {
                    previous = 1;
                    continue;
                }
                buffer = previous + pre_previous;
                pre_previous = previous;
                previous = buffer;
            }
            return previous;
        }
    }
}