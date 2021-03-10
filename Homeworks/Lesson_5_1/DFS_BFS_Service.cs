using System;
using System.Collections.Generic;
using Lesson_4_2;

namespace Lesson_5_1
{
    public class DFS_BFS_Service
    {

        static void Main(string[] args)
        {
            ITree treeData = new Tree(25);
            DFS_BFS_Service service = new DFS_BFS_Service();
            Vertex root = treeData.GetRoot();
            
            root.Print();
            Console.WriteLine("\nDepth First Search:");
            service.DepthFirstSearch(root, service.PrintVertexValue);
            root.Print();
            Console.WriteLine("\nBreadth First Search:");
            service.BreadthFirstSearch(root, service.PrintVertexValue);
            Console.WriteLine("Нажмите любую клавишу для завершения программы");
            Console.ReadKey();
        }
        
        public void PrintVertexValue(Vertex vertex) => Console.WriteLine($"Visiting {vertex.Value} vertex.");
        public void DepthFirstSearch(Vertex root, Action<Vertex> actionWhileSearch)
        {
            /*root.Print();
            Console.WriteLine("\nDepth First Search:");*/
            Stack<Vertex> vertexStack = new Stack<Vertex>();
            vertexStack.Push(root);
            while (vertexStack.Count > 0)
            {
                Vertex vertex = vertexStack.Pop();

                actionWhileSearch?.Invoke(vertex);
                
                if (vertex.Right!=null)
                    vertexStack.Push(vertex.Right);
                if (vertex.Left!=null)
                    vertexStack.Push(vertex.Left);
            }
        }
        
        public void BreadthFirstSearch(Vertex root, Action<Vertex> actionWhileSearch)
        {
            /*root.Print();
            Console.WriteLine("\nBreadth First Search:");*/
            Queue<Vertex> vertexQueue = new Queue<Vertex>();
            vertexQueue.Enqueue(root);
            while (vertexQueue.Count>0)
            {
                Vertex vertex = vertexQueue.Dequeue();
                actionWhileSearch?.Invoke(vertex);
                
                if (vertex.Left!=null)
                    vertexQueue.Enqueue(vertex.Left);
                if (vertex.Right!=null)
                    vertexQueue.Enqueue(vertex.Right);
            }
        }
    }
}