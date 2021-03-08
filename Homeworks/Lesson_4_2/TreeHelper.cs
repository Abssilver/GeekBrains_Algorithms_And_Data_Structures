using System.Collections.Generic;

namespace Lesson_4_2
{
    public static class TreeHelper
    {
        public static VertexInfo[] GetTreeInLine(ITree tree)
        {
            var buffer = new Queue<VertexInfo>();
            var returnArray = new List<VertexInfo>();
            var root = new VertexInfo { Vertex = tree.GetRoot() };
            buffer.Enqueue(root);

            while (buffer.Count != 0)
            {
                var element = buffer.Dequeue();
                returnArray.Add(element);

                var depth = element.Depth + 1;

                if (element.Vertex.Left != null)
                {
                    var left = new VertexInfo
                    {
                        Vertex = element.Vertex.Left,
                        Depth = depth,
                    };
                    buffer.Enqueue(left);
                }
                if (element.Vertex.Right != null)
                {
                    var right = new VertexInfo
                    {
                        Vertex = element.Vertex.Right,
                        Depth = depth,
                    };
                    buffer.Enqueue(right);
                }
            }
            return returnArray.ToArray();
        }
    }
}