using System;

namespace Lesson_7_1
{
    public class RouteSearchService
    {
        static void Main(string[] args)
        {
            RouteSearchService routeSearcher = new RouteSearchService();
            var fieldSize = (8, 10);
            int[,] matrixField = routeSearcher.GenerateField(fieldSize);
            Console.WriteLine("Generated matrix field:");
            routeSearcher.DisplayField(matrixField);
            Console.WriteLine(new string('=', fieldSize.Item2 * 6));
            routeSearcher.FindRoutes(matrixField);
            Console.WriteLine("Route matrix field");
            routeSearcher.DisplayField(matrixField);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private int[,] GenerateField((int x, int y) size)
        {
            Random rnd = new Random();
            int[] probability = { 0, 1, 1, 1, 1 };
            int[,] fieldToReturn = new int[size.x, size.y];
            for (int i = 0; i < fieldToReturn.GetLength(0); i++)
                for (int j = 0; j < fieldToReturn.GetLength(1); j++)
                    fieldToReturn[i,j] = probability[rnd.Next(0, probability.Length)];
            fieldToReturn[0, 0] = 1;
            fieldToReturn[size.x - 1, size.y - 1] = 1;
            return fieldToReturn;
        }

        public void FindRoutes(int[,] field)
        {
            int i, j;
            for (j = 1; j < field.GetLength(1); j++)
                if (field[0, j - 1] == 0)
                    field[0, j] = field[0, j - 1];
            for (i = 1; i < field.GetLength(0); i++)
            {
                if (field[i - 1, 0] == 0)
                    field[i, 0] = field[i - 1, 0];
                for (j = 1; j < field.GetLength(1); j++)
                {
                    if (field[i, j] != 0)
                        field[i, j] = field[i, j - 1] + field[i - 1, j];
                }
            }
        }

        private void DisplayField(int[,] field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == 0)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.White; 
                    Console.Write($"{field[i, j], 5} ");
                }
                Console.WriteLine();
            }
        }
    }
}