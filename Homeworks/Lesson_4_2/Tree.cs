using System;
using System.Collections.Generic;

namespace Lesson_4_2
{
    public class Tree: ITree
    {
        private Vertex _root = null;
        public Tree() { }
        
        public Tree(int rootValue, int numberOfElements): this()
        {
            List<int> sequence = GenerateSequence(numberOfElements, rootValue);
            sequence.ForEach(AddItem);
        }
        public Vertex GetRoot() => _root;
        public Vertex Insert(Vertex vertexToPlaceIn, int value, Vertex root)
        {
            if(vertexToPlaceIn is null) return GetEmptyVertex(value, root);
            if(value < vertexToPlaceIn.Value)
                vertexToPlaceIn.Left = Insert(vertexToPlaceIn.Left, value, vertexToPlaceIn);
            else if(value > vertexToPlaceIn.Value)
                vertexToPlaceIn.Right = Insert(vertexToPlaceIn.Right, value, vertexToPlaceIn);
            else
                throw new ArgumentException
                    ($"Вставляемый элемент присутствует в дереве! Значение {value} не добавлено!");
            return BalanceVertex(vertexToPlaceIn);
        }
        private Vertex FindMin(Vertex root) => root.Left is null ? root : FindMin(root.Left);
        private Vertex RemoveMin(Vertex root)
        {
            if (root.Left is null)
                return root.Right;
            root.Left = RemoveMin(root.Left);
            return BalanceVertex(root);
        }

        public Vertex Remove(Vertex root, int value)
        {
            if (root is null) return null;
            if (value < root.Value)
                root.Left = Remove(root.Left, value);
            else if (value > root.Value)
                root.Right = Remove(root.Right, value);
            else
            {
                Vertex left = root.Left;
                Vertex right = root.Right;
                if (right is null)
                    return left;
                Vertex min = FindMin(right);
                min.Right = RemoveMin(right);
                min.Left = left;
                return BalanceVertex(min);
            }
            return BalanceVertex(root);
        }
        private int GetHeight(Vertex vertex) => vertex?.Height ?? 0;
        private int GetBalanceFactor(Vertex vertex) => GetHeight(vertex.Right) - GetHeight(vertex.Left);
        private void FixHeight(Vertex vertex)
        {
            int leftHeight = GetHeight(vertex.Left);
            int rightHeight = GetHeight(vertex.Right);
            vertex.Height = (leftHeight > rightHeight ? leftHeight : rightHeight) + 1;

        }
        private Vertex RotateRight(Vertex vertex)
        {
            Vertex rotated = vertex.Left;
            vertex.Left = rotated.Right;
            if (rotated.Right is not null)
                rotated.Right.Root = vertex.Left;
            rotated.Right = vertex;
            Vertex vertexRoot = vertex.Root;
            vertex.Root = rotated;
            rotated.Root = vertexRoot;
            FixHeight(vertex);
            FixHeight(rotated);
            return rotated;
        }
        private Vertex RotateLeft(Vertex vertex)
        {
            Vertex rotated = vertex.Right;
            vertex.Right = rotated.Left;
            if (rotated.Left is not null) 
                rotated.Left.Root = vertex.Right;
            rotated.Left = vertex;
            Vertex vertexRoot = vertex.Root;
            vertex.Root = rotated;
            rotated.Root = vertexRoot;
            FixHeight(vertex);
            FixHeight(rotated);
            return rotated;
        }
        private Vertex BalanceVertex(Vertex vertexToBalance)
        {
            FixHeight(vertexToBalance);
            if (GetBalanceFactor(vertexToBalance) == 2)
            {
                if (GetBalanceFactor(vertexToBalance.Right) < 0)
                    vertexToBalance.Right = RotateRight(vertexToBalance.Right);
                return RotateLeft(vertexToBalance);
            }
            if (GetBalanceFactor(vertexToBalance) == -2)
            {
                if (GetBalanceFactor(vertexToBalance.Left) > 0)
                    vertexToBalance.Left = RotateLeft(vertexToBalance.Left);
                return RotateRight(vertexToBalance);
            }
            return vertexToBalance;
        }
        public void AddItem(int value) =>_root = Insert(_root, value, _root);
        public void RemoveItem(int value)
        {
            if (_root is null)
                throw new InvalidOperationException("Tree is empty! Unable to remove an element!");
            Vertex root = Remove(_root, value);
            _root = root ?? 
                    throw new ArgumentException("Unable to remove an element that is not presented into tree!");
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
                Height = 1,
            };
    }
}