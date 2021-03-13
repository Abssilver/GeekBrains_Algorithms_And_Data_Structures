using System;
using System.Collections.Generic;

namespace Lesson_6_1
{
    public class BFS_DFS_InAGraph
    {
        static void Main(string[] args)
        {
            BFS_DFS_InAGraph searchService = new BFS_DFS_InAGraph();
            INode dfsGraph = searchService.GetDFSGraph();
            INode dfsGraphCopy = searchService.GetDFSGraph();
            INode bfsGraph = searchService.GetBFSGraph();
            Console.WriteLine("Depth First Search (Recursion):");
            searchService.DepthFirstSearchRecursion(dfsGraph, searchService.PrintNodeVertexIntoConsole);
            Console.WriteLine("Depth First Search (Stack):");
            searchService.DepthFirstSearch(dfsGraphCopy, searchService.PrintNodeVertexIntoConsole);
            Console.WriteLine("Breadth First Search:");
            searchService.BreadthFirstSearch(bfsGraph, searchService.PrintNodeVertexIntoConsole);
            Console.WriteLine("Нажмите любую клавишу для завершения программы");
        }

        public void DepthFirstSearchRecursion(INode nodeToStartIn, Action<INode> actionWhileSearching)
        {
            if (nodeToStartIn.Visited)
                return;
            
            nodeToStartIn.Visited = true;
            actionWhileSearching?.Invoke(nodeToStartIn);
            nodeToStartIn.Edges.ForEach(edge => DepthFirstSearchRecursion(edge, actionWhileSearching));
        }

        public void DepthFirstSearch(INode nodeToStartIn, Action<INode> actionWhileSearching)
        {
            Stack<INode> traverse = new Stack<INode>();
            traverse.Push(nodeToStartIn);
            nodeToStartIn.Visited = true;
            while (traverse.Count > 0)
            {
                INode step = traverse.Pop();
                actionWhileSearching?.Invoke(step);
                step.Edges.ForEach(edge =>
                {
                    if (!edge.Visited)
                    {
                        traverse.Push(edge);
                        edge.Visited = true;
                    }
                });
            }
        }

        public void PrintNodeVertexIntoConsole(INode node) => 
            Console.WriteLine($"Visiting {node.Vertex} vertex.");


        public void BreadthFirstSearch(INode nodeToStartIn, Action<INode> actionWhileSearching)
        {
            Queue<INode> traverse = new Queue<INode>();
            traverse.Enqueue(nodeToStartIn);
            nodeToStartIn.Visited = true;
            while (traverse.Count > 0)
            {
                INode step = traverse.Dequeue();
                actionWhileSearching?.Invoke(step);
                step.Edges.ForEach(edge =>
                {
                    if (!edge.Visited)
                    {
                        traverse.Enqueue(edge);
                        edge.Visited = true;
                    }
                });
            }
        }
        
        #region CreateData
        public INode GetDFSGraph()
        {
            INode first = new Node { Vertex = 1, Visited = false };
            INode second = new Node { Vertex = 2, Visited = false };
            INode third = new Node { Vertex = 3, Visited = false };
            INode fourth = new Node { Vertex = 4, Visited = false };
            INode fifth = new Node { Vertex = 5, Visited = false };
            INode sixth = new Node { Vertex = 6, Visited = false };
            INode seventh = new Node { Vertex = 7, Visited = false };
            INode eigth = new Node { Vertex = 8, Visited = false };

            first.AddEdge(second);
            first.AddEdge(fourth);
            second.AddEdge(first);
            second.AddEdge(third);
            second.AddEdge(fourth);
            third.AddEdge(second);
            third.AddEdge(fifth);
            fourth.AddEdge(first);
            fourth.AddEdge(sixth);
            fifth.AddEdge(third);
            fifth.AddEdge(seventh);
            sixth.AddEdge(fourth);
            sixth.AddEdge(seventh);
            sixth.AddEdge(eigth);
            seventh.AddEdge(fifth);
            seventh.AddEdge(sixth);
            eigth.AddEdge(sixth);

            //      1  - 2 - 3 - 5
            //      |    |       |
            //      |    |    8  7
            //       \   |     \ |
            //        -  4 - - - 6
            
            return first;
        }
        
        public INode GetBFSGraph()
        {
            INode first = new Node { Vertex = 1, Visited = false };
            INode second = new Node { Vertex = 2, Visited = false };
            INode third = new Node { Vertex = 3, Visited = false };
            INode fourth = new Node { Vertex = 4, Visited = false };
            INode fifth = new Node { Vertex = 5, Visited = false };
            INode sixth = new Node { Vertex = 6, Visited = false };
            INode seventh = new Node { Vertex = 7, Visited = false };
            INode eigth = new Node { Vertex = 8, Visited = false };
            
            first.AddEdge(second);
            first.AddEdge(third);
            second.AddEdge(first);
            second.AddEdge(fourth);
            third.AddEdge(first);
            third.AddEdge(fourth);
            fourth.AddEdge(second);
            fourth.AddEdge(third);
            fourth.AddEdge(fifth);
            fifth.AddEdge(fourth);
            fifth.AddEdge(sixth);
            fifth.AddEdge(eigth);
            sixth.AddEdge(fifth);
            sixth.AddEdge(seventh);
            seventh.AddEdge(sixth);
            eigth.AddEdge(fifth);

            //      1 - 2 - 4 - 5 - 6 - 7
            //       \     /     \
            //        3 --        8
            
            return first;
        }
        #endregion
    }
}