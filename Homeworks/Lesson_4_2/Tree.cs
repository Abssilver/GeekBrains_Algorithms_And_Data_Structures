using System;
using System.Collections.Generic;

namespace Lesson_4_2
{
    public class Tree: ITree
    {
        private Vertex _root = null;
        public Tree() { }
        
        public Tree(int rootValue): this()
        {
            List<int> sequence = GenerateSequence(10, rootValue);
            sequence.ForEach(AddItem);
        }
        public Vertex GetRoot() => _root;

        public void AddItem(int value)
        {
            if (_root == null)
            {
                _root = GetEmptyVertex(value, null);
                return;
            }
            Vertex tmp = _root;
            while (tmp != null)
            {
                if (value > tmp.Value)
                {
                    if (tmp.Right != null)
                    {
                        tmp = tmp.Right;
                        continue;
                    }

                    tmp.Right = GetEmptyVertex(value, tmp);
                    return;
                }

                if (value < tmp.Value)
                {
                    if (tmp.Left != null)
                    {
                        tmp = tmp.Left;
                        continue;
                    }

                    tmp.Left = GetEmptyVertex(value, tmp);
                    return;
                }
                
                Console.WriteLine("Вставляемый элемент присутствует в дереве! Значение не добавлено!");
                tmp = null;
            }
        }

        public void RemoveItem(int value)
        {
            Vertex toRemove = GetVertexByValue(value);
            if (toRemove == null)
                return;
            
            if (IsLeaf(toRemove))
            {
                if (toRemove.Root!=null)
                {
                    ReplaceVertex(toRemove, null);
                    return;
                }
                _root = null;
                return;
            }
            if (HasOnlyOneChild(toRemove))
            {
                Vertex newVertex = toRemove.Right ?? toRemove.Left;
                if (toRemove.Root!=null)
                {
                    ReplaceVertex(toRemove, newVertex);
                    return;
                }
                newVertex.Root = null;
                _root = newVertex;
                return;
            }
            
            Vertex toPlaceOntoRemoved = toRemove.Right;
            while (toPlaceOntoRemoved.Left != null)
            {
                toPlaceOntoRemoved = toPlaceOntoRemoved.Left;
            }

            if (!toPlaceOntoRemoved.Equals(toRemove.Right))
            {
                if (toPlaceOntoRemoved.Right != null)
                {
                    toPlaceOntoRemoved.Root.Left = toPlaceOntoRemoved.Right;
                    toPlaceOntoRemoved.Right.Root = toPlaceOntoRemoved.Root;
                }
                else
                {
                    toPlaceOntoRemoved.Root.Left = null;
                }
                toPlaceOntoRemoved.Right = toRemove.Right;
                toRemove.Right.Root = toPlaceOntoRemoved;
            }
                
            toPlaceOntoRemoved.Left = toRemove.Left;
            toRemove.Left.Root = toPlaceOntoRemoved;
            toPlaceOntoRemoved.Root = toRemove.Root;
            if (toRemove.Root!=null)
            {
                ReplaceVertex(toRemove, toPlaceOntoRemoved);
            }
            else
            {
                _root = toPlaceOntoRemoved;
            }
        }
        private List<int> GenerateSequence(int length, int rootValue)
        {
            if (length <= 0)
                return null;
            List<int> toReturn = new List<int>();
            toReturn.Add(rootValue);
            int valuesAdded = 1;
            Random rnd = new Random();
            while (valuesAdded < length)
            {
                int valueToAdd = rnd.Next(0, rootValue * 2);
                if (!toReturn.Contains(valueToAdd))
                {
                    toReturn.Add(valueToAdd);
                    valuesAdded++;
                }
            }
            return toReturn;
        }
        private bool IsLeaf(Vertex vertex) => vertex.Left == null && vertex.Right == null;
        private bool HasOnlyOneChild(Vertex vertex) => vertex.Left!=null ^ vertex.Right!=null;
        
        private void ReplaceVertex(Vertex from, Vertex to)
        {
            if (from.Root.Left!=null && from.Root.Left.Equals(from)) 
                from.Root.Left = to;
            else
                from.Root.Right = to;
        }

        public Vertex GetVertexByValue(int value) => GetVertexByValue(value, _root);
        private Vertex GetVertexByValue(int value, Vertex compareWith)
        {
            Vertex toReturn = null;
            if (compareWith!=null)
            {
                if (compareWith.Value > value)
                    toReturn = GetVertexByValue(value, compareWith.Left);
                else if (compareWith.Value < value)
                    toReturn = GetVertexByValue(value, compareWith.Right);
                else 
                    return compareWith;
            }
            return toReturn;
        }
        public void PrintTree()
        {
            _root.Print();
        }

        public void PrintSecondVariant()
        {
            _root.Print("", VertexPosition.Center, true, false);
        }

        private Vertex GetEmptyVertex(int value, Vertex root) =>
            new()
            {
                Value = value,
                Left = null,
                Right = null,
                Root = root,
            };
    }
}