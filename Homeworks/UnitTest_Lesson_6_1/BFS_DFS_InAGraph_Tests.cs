using System.Collections.Generic;
using Lesson_6_1;
using NUnit.Framework;

namespace UnitTest_Lesson_6_1
{
    [TestFixture]
    public class BFS_DFS_InAGraph_Tests
    {
        private BFS_DFS_InAGraph _searchService;
        private int[] _dfsVertexArray;
        private int[] _dfsInReverseVertexArray;
        private int[] _bfsVertexArray;

        [SetUp]
        public void Setup()
        {
            _searchService = new BFS_DFS_InAGraph();
            _dfsVertexArray = new []{1, 2, 3, 5, 7, 6, 4, 8};
            _dfsInReverseVertexArray = new []{1, 4, 6, 8, 7, 5, 3, 2};
            _bfsVertexArray = new []{1, 2, 3, 4, 5, 6, 8, 7};
        }

        #region CreateData
        
        private INode GetDFSGraph()
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

        private INode GetBFSGraph()
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

        [Test]
        public void DepthFirstSearchRecursion_AreEqual_ArrayDFSAndListValues_ReturnTrue()
        {
            List<int> actual = new List<int>();
            INode dfsRoot = GetDFSGraph();
            _searchService.DepthFirstSearchRecursion(dfsRoot, node =>
            {
                actual.Add(node.Vertex);
            });
            Assert.AreEqual(true, AreEqual(array:_dfsVertexArray, list: actual));
        }
        
        [Test]
        public void DepthFirstSearchStack_AreEqual_ArrayDFSAndListValues_ReturnTrue()
        {
            List<int> actual = new List<int>();
            INode dfsRoot = GetDFSGraph();
            _searchService.DepthFirstSearch(dfsRoot, node =>
            {
                actual.Add(node.Vertex);
            });
            Assert.AreEqual(true, AreEqual(array:_dfsInReverseVertexArray, list: actual));
        }

        [Test]
        public void BreadthFirstSearch_AreEqual_ArrayDFSAndListValues_ReturnTrue()
        {
            List<int> actual = new List<int>();
            INode bfsRoot = GetBFSGraph();
            _searchService.BreadthFirstSearch(bfsRoot, node =>
            {
                actual.Add(node.Vertex);
            });
            Assert.AreEqual(true, AreEqual(array:_bfsVertexArray, list: actual));
        }

        private bool AreEqual(int[] array, List<int> list)
        {
            if (array.Length == list.Count)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i]!=list[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}