using System.IO;
using System.Collections.Generic;
using System;
using Color = Microsoft.Msagl.Drawing.Color;
using Node = Microsoft.Msagl.Drawing.Node;

namespace TubesStima2
{
    public class BreadthFirstSearch
    {
        public List<string> Solution;
        public BreadthFirstSearch()
        {
            this.Solution = new List<string>();
        }
        public String[] SplitPath(string path)
        {
            String[] pathSeparators = new String[] { "\\" };
            return path.Split(pathSeparators, StringSplitOptions.RemoveEmptyEntries);
        }

        public Boolean BFS(string root, string searchValue, bool allOccurence, DrawingTree t)
        {
            Boolean FOUND = false;
            Queue<string> dirs = new Queue<string>();
            Queue<Node> nodes = new Queue<Node>();
            // Queue<DrawingTree> dirNode = new Queue<DrawingTree>();
            dirs.Enqueue(root);
            nodes.Enqueue(t.Graph.FindNode(t.getID));
            // dirNode.Enqueue(t);
            string[] files = null;
            string[] subDirs = null;

            while (dirs.Count > 0 && nodes.Count > 0 && (!FOUND || allOccurence))
            {
                string current = dirs.Dequeue();
                // DrawingTree currentTree = dirNode.Dequeue();
                Node currentNode = nodes.Dequeue();
                // process all files directly under this folder
                try
                {
                    files = System.IO.Directory.GetFiles(current);
                    if (files.Length == 0)
                    {
                        // currentTree.UpdateEmptyFolderColor(currentTree.getID);
                        currentNode.Label.FontColor = Color.Red;
                    }
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                // main process
                if (files != null)
                {
                    foreach (string fi in files)
                    {
                        
                        if (Path.GetFileName(fi) == searchValue)
                        {
                            Solution.Add(fi);
                            string LastName = SplitPath(fi)[SplitPath(fi).Length - 1];
                            // currentTree.AddChild(LastName, Color.Green);
                            t.AddChild(currentNode.Id, LastName, Color.Green);
                            FOUND = true;

                            // Jika all Occurence tidak di cek
                            if (!allOccurence) break;
                        }
                        else
                        {
                            // currentTree.AddChild(Path.GetFileName(fi), Color.Red);
                            t.AddChild(currentNode.Id, Path.GetFileName(fi), Color.Red);
                        }
                    }
                    try
                    {
                        subDirs = System.IO.Directory.GetDirectories(current);
                    }
                    catch (System.IO.DirectoryNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    foreach (string dirInfo in subDirs)
                    {
                        // this.QUEUE.Push(dirInfo)
                        dirs.Enqueue(dirInfo);
                        string LastName = SplitPath(dirInfo)[SplitPath(dirInfo).Length - 1];
                        // DrawingTree t1 = new DrawingTree(LastName, Color.Black);
                        // currentTree.AddChild(t1);
                        string childId = t.AddChild(currentNode.Id, LastName, Color.Black);
                        // dirNode.Enqueue(t1);
                        nodes.Enqueue(t.Graph.FindNode(childId));
                    }
                }
            }


            return FOUND;
        }
    }
}