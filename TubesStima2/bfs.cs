using System.IO;
using System.Collections.Generic;
using System;
using Color = Microsoft.Msagl.Drawing.Color;

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

        public Boolean BFS(string root, string searchValue, bool allOccurence, DrawingTree t, Boolean FOUND)
        {
            Queue<string> dirs = new Queue<string>();
            Queue<DrawingTree> dirtree = new Queue<DrawingTree>();
            dirs.Enqueue(root);
            dirtree.Enqueue(t);
            string[] files = null;
            string[] subDirs = null;

            while (dirs.Count > 0 && dirtree.Count > 0 && (!FOUND || allOccurence))
            {
                string current = dirs.Dequeue();
                DrawingTree currentTree = dirtree.Dequeue();
                // process all files directly under this folder
                try
                {
                    files = System.IO.Directory.GetFiles(current);
                    if (files.Length == 0)
                    {
                        currentTree.UpdateEmptyFolderColor(currentTree.getID);
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
                        Console.WriteLine(fi);
                        if (Path.GetFileName(fi) == searchValue)
                        {
                            Solution.Add(fi);
                            string LastName = SplitPath(fi)[SplitPath(fi).Length - 1];
                            currentTree.AddChild(LastName, Color.Green);
                            FOUND = true;

                            // Jika all Occurence tidak di cek
                            if (!allOccurence) break;
                        }
                        else
                        {
                            currentTree.AddChild(Path.GetFileName(fi), Color.Red);
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
                        DrawingTree t1 = new DrawingTree(LastName, Color.Black);
                        currentTree.AddChild(t1);
                        dirtree.Enqueue(t1);
                    }
                }
            }


            return FOUND;
        }
    }
}