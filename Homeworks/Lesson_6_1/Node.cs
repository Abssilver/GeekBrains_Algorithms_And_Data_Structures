using System.Collections.Generic;

namespace Lesson_6_1
{
    public class Node: INode
    {
        public int Vertex { get; set; }
        public bool Visited { get; set; }
        public void AddEdge(INode toAdd) => Edges.Add(toAdd);
        public List<INode> Edges { get; } = new List<INode>();
    }

    public interface INode
    {
        int Vertex { get; set; }
        bool Visited { get; set; }
        void AddEdge(INode toAdd);
        List<INode> Edges { get; }
    }
}
