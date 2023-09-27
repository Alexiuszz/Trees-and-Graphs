using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BreadthFirstSearch
{
    class BreadthFirstSearch
    {
        /// <summary>
        /// Traverses and prints given directory with BFS
        /// </summary>
        /// <param name="directoryPath">the path to the directory /// which should be traversed</param>
        static void TraverseDir(string directoryPath)
        {
            Queue<DirectoryInfo> visitedDirsQueue =
            new Queue<DirectoryInfo>();
            visitedDirsQueue.Enqueue(new DirectoryInfo(directoryPath));
            while (visitedDirsQueue.Count > 0)
            {
                DirectoryInfo currentDir = visitedDirsQueue.Dequeue();
                Console.WriteLine(currentDir.FullName);
                DirectoryInfo[] children = currentDir.GetDirectories();
                foreach (DirectoryInfo child in children)
                {
                    visitedDirsQueue.Enqueue(child);
                }
                
            }
        }

        static void DFSTraverseDir(string directoryPath)
        {
            if(directoryPath == null)
            {
                return;
            }           
            DirectoryInfo currentDir = new DirectoryInfo(directoryPath);
            Console.WriteLine(currentDir.FullName);
            DirectoryInfo[] directories = currentDir.GetDirectories();

            foreach(DirectoryInfo directory in directories)
            {
                DFSTraverseDir(directory.FullName);
            }

                
        }
        static void Main(string[] args)
        {
            TraverseDir(@"C:\");
            DFSTraverseDir(@"C:\");
        }
    }
}
