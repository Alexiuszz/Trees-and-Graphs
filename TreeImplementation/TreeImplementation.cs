using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeImplementation
{
    /// <summary>Represents a tree node</summary>
    /// <typeparam name="T">the type of the values in nodes
    /// </typeparam>
    public class TreeNode<T>
    {
        // Contains the value of the node
        private T value;
        // Shows whether the current node has a parent or not
        private bool hasParent;
        // Contains the children of the node (zero or more)
        private List<TreeNode<T>> children;

        /// <summary>Constructs a tree node</summary>
        /// <param name="value">the value of the node</param>
        public TreeNode(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(
                "Cannot insert null value!");
            }
            this.value = value;
            this.children = new List<TreeNode<T>>();
        }

        /// <summary>The value of the node</summary>
        public T Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        /// <summary>The number of node's children</summary>
        public int ChildrenCount
        {
            get
            {
                return this.children.Count;
            }
        }

        /// <summary>Adds child to the node</summary>
        /// <param name="child">the child to be added</param>
        public void AddChild(TreeNode<T> child)
        {
            if (child == null)
            {
                throw new ArgumentNullException(
                "Cannot insert null value!");
            }
            if (child.hasParent)
            {
                throw new ArgumentException(
                "The node already has a parent!");
            }
            child.hasParent = true;
            this.children.Add(child);
        }

        /// <summary>
        /// Gets the child of the node at given index
        /// </summary>
        /// <param name="index">the index of the desired child</param>
        /// <returns>the child on the given position</returns>
        public TreeNode<T> GetChild(int index)
        {
            return this.children[index];
        }
    }

    /// <summary>Represents a tree data structure</summary>
    /// <typeparam name="T">the type of the values in the
    /// tree</typeparam>
    public class Tree<T>
    {
        // The root of the tree
        private TreeNode<T> root;

        /// <summary>Constructs the tree</summary>
        /// <param name="value">the value of the node</param>
        public Tree(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(
                "Cannot insert null value!");
            }
            this.root = new TreeNode<T>(value);
        }

        /// <summary>Constructs the tree</summary>
        /// <param name="value">the value of the root node</param>
        /// <param name="children">the children of the root
        /// node</param>
        public Tree(T value, params Tree<T>[] children)
        : this(value)
        {
            foreach (Tree<T> child in children)
            {
                this.root.AddChild(child.root);
            }
        }

        /// <summary>
        /// The root node or null if the tree is empty
        /// </summary>
        public TreeNode<T> Root
        {
            get
            {
                return this.root;
            }
        }

        /// <summary>Traverses and prints tree in 
        /// Depth-First Search (DFS) manner</summary>
        /// <param name="root">the root of the tree to be
        /// traversed</param>
        /// <param name="spaces">the spaces used for
        /// representation of the parent-child relation</param>
        private void PrintDFS(TreeNode<T> root, string spaces)
        {
            if (this.root == null)
            {
                return;
            }
            Console.WriteLine(spaces + root.Value);
            TreeNode<T> child = null;
            for (int i = 0; i < root.ChildrenCount; i++)
            {
                child = root.GetChild(i);
                PrintDFS(child, spaces + " ");
            }
        }

        /// <summary>Traverses and prints the tree in
        /// Depth-First Search (DFS) manner</summary>
        public void TraverseDFS()
        {
            this.PrintDFS(this.root, string.Empty);
        }

        private void PrintBFS(TreeNode<T> node)
        {
            Queue<TreeNode<T>> nodes = new Queue<TreeNode<T>>();
            nodes.Enqueue(node);
            while(nodes.Count > 0)
            {
                TreeNode<T> currentNode = nodes.Dequeue();
                Console.WriteLine(currentNode.Value);
                for(int i = 0; i < currentNode.ChildrenCount; i++)
                {
                    nodes.Enqueue(currentNode.GetChild(i));
                }
            }
        }

        public void TraverseBFS()
        {
            PrintBFS(this.root);
        }

        static int theCount;
        public int NumOfOccurances(int num)
        {
            //theCount = 0;
            TraverseDFS(num, root); 
            return theCount;
        }

        /// <summary>
        /// Counts the occurences of a value in the tree traversing using DFS 
        /// </summary>
        /// <param name="num"> the value to count </param>
        /// <param name="node"> the node to check </param>
        private void TraverseDFS(int num, TreeNode<T> node)
        {
            if (node == null)
            {
                return;
            }
            if (node.Value.Equals(num))
            {
                theCount++;
            }

            TreeNode<T> child = null;
            for (int i = 0; i < node.ChildrenCount; i++)
            {
                child = node.GetChild(i);
                TraverseDFS(num, child);
            }
        }              

        static List<T> subRoots = new List<T>();
        public void RootWithKNode(int numberOfNodes)
        {
            RootWithKNodeDFS(numberOfNodes, root);
            Console.WriteLine("The Subroots with " + numberOfNodes + " nodes: ");
            for (int i = 0; i < subRoots.Count; i++)
            {               
                Console.WriteLine(subRoots[i]);
            }
        }

        private void RootWithKNodeDFS(int k, TreeNode<T> node)
        {
            if(node == null)
            {
                return;
            }

            if(node.ChildrenCount == k)
            {
                subRoots.Add(node.Value);
            }

            TreeNode<T> child = null;
            for(int i = 0; i < node.ChildrenCount; i++)
            {
                child = node.GetChild(i); 
                RootWithKNodeDFS(k, child);
            }
        }
        
    }

    
    
    class TreeImplementation
    {
        static void Main(string[] args)
        {
            // Create the tree from the sample
            Tree<int> tree =
                new Tree<int>(7,
                    new Tree<int>(19,
                        new Tree<int>(1),
                        new Tree<int>(19),
                        new Tree<int>(31)),
                    new Tree<int>(21),
                    new Tree<int>(19, 
                        new Tree<int>(23),
                        new Tree<int>(6))
            );

            // Traverse and print the tree using Depth-First-Search
            Console.WriteLine("Number of 19 Nodes == " + tree.NumOfOccurances(19));
            tree.RootWithKNode(2);

            Console.WriteLine("printing tree");
            tree.TraverseDFS();
            tree.TraverseBFS(); 

        }
    }
}
