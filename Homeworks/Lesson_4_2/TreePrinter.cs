using System;
using System.Collections.Generic;

namespace Lesson_4_2
{
    public static class TreePrinter
    {
        class VertexData
        {
            public Vertex Vertex;
            public string Text;
            public int StartPos;
            private int Size => Text.Length;
            public int EndPos
            {
                get => StartPos + Size; 
                set => StartPos = value - Size;
            }

            public VertexData Parent, Left, Right;
        }

        public static void Print(this Vertex root, string textFormat = "(0)", int spacing = 2, int topMargin = 2,
            int leftMargin = 2)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<VertexData>();
            var next = root;
            for (int depth = 0; next != null; depth++)
            {
                var vert = new VertexData {Vertex = next, Text = next.Value.ToString(textFormat)};
                if (depth < last.Count)
                {
                    vert.StartPos = last[depth].EndPos + spacing;
                    last[depth] = vert;
                }
                else
                {
                    vert.StartPos = leftMargin;
                    last.Add(vert);
                }

                if (depth > 0)
                {
                    vert.Parent = last[depth - 1];
                    if (next == vert.Parent.Vertex.Left)
                    {
                        vert.Parent.Left = vert;
                        vert.EndPos = Math.Max(vert.EndPos, vert.Parent.StartPos - 1);
                    }
                    else
                    {
                        vert.Parent.Right = vert;
                        vert.StartPos = Math.Max(vert.StartPos, vert.Parent.EndPos + 1);
                    }
                }

                next = next.Left ?? next.Right;
                for (; next == null; vert = vert.Parent)
                {
                    int top = rootTop + 2 * depth;
                    Print(vert.Text, top, vert.StartPos);
                    if (vert.Left != null)
                    {
                        Print("/", top + 1, vert.Left.EndPos);
                        Print("_", top, vert.Left.EndPos + 1, vert.StartPos);
                    }

                    if (vert.Right != null)
                    {
                        Print("_", top, vert.EndPos, vert.Right.StartPos - 1);
                        Print("\\", top + 1, vert.Right.StartPos - 1);
                    }

                    if (--depth < 0) break;
                    if (vert == vert.Parent.Left)
                    {
                        vert.Parent.StartPos = vert.EndPos + 1;
                        next = vert.Parent.Vertex.Right;
                    }
                    else
                    {
                        if (vert.Parent.Left == null)
                            vert.Parent.EndPos = vert.StartPos - 1;
                        else
                            vert.Parent.StartPos += (vert.StartPos - 1 - vert.Parent.EndPos) / 2;
                    }
                }
            }

            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }

        private static void Print(string s, int top, int left, int right = -1)
        {
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            while (Console.CursorLeft < right) Console.Write(s);
        }
    }
}