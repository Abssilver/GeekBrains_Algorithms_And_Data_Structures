using System;

namespace Lesson_2_1
{
    class Program
    {
        static void Main(string[] args)
        { }
    }

    public class TwoLinkedList : ILinkedList
    {
        private Node _head = null;
        private Node _tail = null;
        /*private int _count;*/

        public TwoLinkedList()
        {
            /*_count = 0;*/
        }
        /*public int GetCount() => _count;*/
        public int GetCount()
        {
            int count = 0;
            if (_head != null)
            {
                count++;
                Node pointer = _head;
                while (pointer.NextNode != null)
                {
                    pointer = pointer.NextNode;
                    count++;
                }
            }
            return count;
        }

        public void AddNode(int value)
        {
            if (_head != null)
            {
                Node buffer = new Node
                {
                    Value = value,
                    PrevNode = _tail,
                    NextNode = null
                };
                _tail.NextNode = buffer;
                _tail = buffer;
            }
            else
            {
                _head = new Node
                {
                    Value = value,
                    PrevNode = null,
                    NextNode = null
                };
                _tail = _head;
            }
            /*_count++;*/
        }
        
        public void AddNodeAfter(Node node, int value)
        {
            Node insertedNode = new Node
            {
                Value = value,
                PrevNode = node,
                NextNode = node.NextNode
            };
            node.NextNode = insertedNode;
            if (node.Equals(_tail))
            {
                _tail = insertedNode;
            }
            /*_count++;*/
        }

        public void RemoveNode(int index)
        {
            if (index < 0 || index >= GetCount())
            {
                throw new ArgumentException("Введено недопустимое значение индекса");
            }
            Node pointer = _head;
            for (int i = 0; i < index; i++)
            {
                pointer = pointer.NextNode;
            }
            if (index > 0)
            {
                pointer.PrevNode.NextNode = pointer.NextNode;
                if (pointer.Equals(_tail))
                {
                    _tail = pointer.PrevNode;
                }
            }
            else
            {
                pointer.NextNode.PrevNode = null;
                _head = pointer.NextNode;
            }
            /*_count--;*/
        }

        public void RemoveNode(Node node)
        {
            if (node.PrevNode != null)
            {
                node.PrevNode.NextNode = node.NextNode;
                if (node.Equals(_tail))
                {
                    _tail = node.PrevNode;
                }
            }
            else
            {
                node.NextNode.PrevNode = null;
                _head = node.NextNode;
            }
            /*_count--;*/
        }

        public Node FindNode(int searchValue)
        {
            Node pointer = _head;
            for (int i = 0; i < GetCount(); i++)
            {
                if (pointer.Value == searchValue)
                {
                    return pointer;
                }
                pointer = pointer.NextNode;
            }
            return null;
        }
    }
    public class Node
    {
        public int Value { get; set; }
        public Node NextNode { get; set; }
        public Node PrevNode { get; set; }
    }
    //Начальную и конечную ноду нужно хранить в самой реализации интерфейса
    public interface ILinkedList
    {
        int GetCount(); // возвращает количество элементов в списке
        void AddNode(int value);  // добавляет новый элемент списка
        void AddNodeAfter(Node node, int value); // добавляет новый элемент списка после определённого элемента
        void RemoveNode(int index); // удаляет элемент по порядковому номеру
        void RemoveNode(Node node); // удаляет указанный элемент
        Node FindNode(int searchValue); // ищет элемент по его значению
    }
}