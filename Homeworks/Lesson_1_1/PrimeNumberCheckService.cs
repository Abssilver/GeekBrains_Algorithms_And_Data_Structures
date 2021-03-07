using System;

namespace Lesson_1_1
{
    public class PrimeNumberCheckService
    {
        static void Main(string[] args)
        {
            //Проверка находится в проекте UnitTest_Lesson_1_1
        }
        public bool PrimeNumberCheck(uint number)
        {
            uint d = 0;
            for (uint i = 2; i < number; i++)
            {
                if (number % i == 0)
                    d++;
            }
            return d == 0;
        }
    }
}