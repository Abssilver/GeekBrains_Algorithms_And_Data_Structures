using System;

namespace Lesson_4_2
{
    class Program
    {
        private static ITree _tree = null;
        private static int _printVariant = 0;
        
        static void Main(string[] args)
        {
            Greetings();
        }

        static void Greetings()
        {
            Console.WriteLine("Программа для работы с двоичным деревом привествует вас!");
            int convertedInput = 0;
            do
            {
                GetUserInput(ref convertedInput);
                ProcessInput(convertedInput);
            } while (convertedInput != 0);

            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }

        static void ProcessInput(int input)
        {
            switch (input)
            {
                case 1:
                    Random rnd = new Random();
                    _tree = new Tree(rnd.Next(20, 50), 20);
                    PrintTree();
                    break;
                case 2:
                    _printVariant = (++_printVariant) % 2;
                    PrintTree();
                    break;
                case 3:
                    int value = 0;
                    GetValue(ref value);
                    AddValue(value);
                    PrintTree();
                    break;
                case 4:
                    int toRemove = 0;
                    GetValue(ref toRemove);
                    RemoveValue(toRemove);
                    PrintTree();
                    break;
                case 5:
                    int toSearch = 0;
                    GetValue(ref toSearch);
                    SearchValue(toSearch);
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("Введено недопустимое значение");
                    break;
            }
        }

        private static void PrintTree()
        {
            if (_tree!=null)
            {
                if (_printVariant == 0)
                {
                    _tree.PrintTree();
                }
                else
                {
                    _tree.PrintSecondVariant();
                }
            }
            else
            {
                Console.WriteLine("Необходимо вначале сгенерировать дерево");
            }
        }

        private static void AddValue(int value)
        {
            if (_tree!=null)
            {
                _tree.AddItem(value);
            }
            else
            {
                _tree = new Tree();
                _tree.AddItem(value);
            }
        }
        private static void RemoveValue(int value)
        {
            if (_tree!=null)
            {
                _tree.RemoveItem(value);
            }
            else
            {
                Console.WriteLine("Необходимо вначале сгенерировать дерево");
            }
        }

        private static void SearchValue(int value)
        {
            if (_tree!=null)
            {
                if (_tree.GetVertexByValue(value)!=null)
                    Console.WriteLine("Значение найдено");
                else
                    Console.WriteLine("Значение не найдено");
            }
            else
            {
                Console.WriteLine("Необходимо вначале сгенерировать дерево");
            }
        }

        private static void GetUserInput(ref int convertedInput)
        {
            do
            {
                Console.WriteLine("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}",
                    "Введите 1 - Для генерации случайных значений дерева",
                    "Введите 2 - Для смены отображения дерева",
                    "Введите 3 - Для добавления элемента",
                    "Введите 4 - Для удаления элемента",
                    "Введите 5 - Для поиска элемента",
                    "Введите 0 - Для завершения программы",
                "Пример ввода: 3");
            } while (!Int32.TryParse(Console.ReadLine(), out convertedInput));
        }
        private static void GetValue(ref int convertedInput)
        {
            do
            {
                Console.WriteLine("{0}\n{1}",
                    "Введите значение",
                    "Пример ввода: 11");
            } while (!Int32.TryParse(Console.ReadLine(), out convertedInput));
        }
    }
}