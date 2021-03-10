using System;

namespace Lesson_4_2
{
    public class Vertex
    {
        public int Value { get; set; }
        public Vertex Left { get; set; }
        public Vertex Right { get; set; }
        public Vertex Root { get; set; }

        public override bool Equals(object obj)
        {
            var vertex = obj as Vertex;

            if (vertex == null)
                return false;

            return vertex.Value == Value;
        }

        private void PrintValue(string value, VertexPosition vertexPostion)
        {
            switch (vertexPostion)
            {
                case VertexPosition.Left:
                    PrintLeftValue(value);
                    break;
                case VertexPosition.Right:
                    PrintRightValue(value);
                    break;
                case VertexPosition.Center:
                    Console.WriteLine(value);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void PrintLeftValue(string value)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("L:");
            Console.ForegroundColor = (value == "-") ? ConsoleColor.Red : ConsoleColor.Gray;
            Console.WriteLine(value);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void PrintRightValue(string value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("R:");
            Console.ForegroundColor = (value == "-") ? ConsoleColor.Red : ConsoleColor.Gray;
            Console.WriteLine(value);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void Print(string spacing, VertexPosition vertexPosition, bool last, bool empty)
        {

            Console.Write(spacing);
            if (last)
            {
                Console.Write("└─");
                spacing += "  ";
            }
            else
            {
                Console.Write("├─");
                spacing += "| ";
            }

            var stringValue = empty ? "-" : Value.ToString();
            PrintValue(stringValue, vertexPosition);

            if (!empty && (this.Left != null || this.Right != null))
            {
                if (this.Left != null)
                    this.Left.Print(spacing, VertexPosition.Left, false, false);
                else
                    Print(spacing, VertexPosition.Left, false, true);

                if (this.Right != null)
                    this.Right.Print(spacing, VertexPosition.Right, true, false);
                else
                    Print(spacing, VertexPosition.Right, true, true);
            }
        }
    }

    public enum VertexPosition
    {
        Left,
        Right,
        Center
    }
}