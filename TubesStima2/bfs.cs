using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using Color = Microsoft.Msagl.Drawing.Color;
using Node = Microsoft.Msagl.Drawing.Node;
using Edge = Microsoft.Msagl.Drawing.Edge;

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
            int maxLevel = 0;
            Queue<string> dirQueue = new Queue<string>();
            Queue<(string, int)> nodeDirQueue = new Queue<(string, int)>();
            dirQueue.Enqueue(root);
            nodeDirQueue.Enqueue((t.getID, 0));
            // Queue<DrawingTree> dirNode = new Queue<DrawingTree>();
            while (dirQueue.Count > 0)
            {
                string currentDir = dirQueue.Dequeue();
                (string, int) currentNode = nodeDirQueue.Dequeue();
                string currentNodeDir = currentNode.Item1;
                int currentLevel = currentNode.Item2;
                string[] files = null;
                string[] subDirs = null;
                if (!FOUND || allOccurence)
                {
                    maxLevel = currentLevel;
                }
                if (currentLevel <= maxLevel)
                {
                    t.UpdateEmptyFolderColor(currentNodeDir, true);
                }
                
                try
                {
                    files = System.IO.Directory.GetFiles(currentDir);

                    //Kasus jika folder kosong, update menjadi warna merah
                    if (files.Length == 0)
                    {
                        t.UpdateEmptyFolderColor(currentNodeDir);
                    }
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                if (files != null)
                {
                    foreach (string fi in files)
                    {
                        string fname = Path.GetFileName(fi);
                        if (FOUND && !allOccurence)
                        {
                            t.AddChild(currentNodeDir, fname, Color.Black, false);
                        }
                        else
                        {
                            if (fname == searchValue)
                            {
                                Solution.Add(fi);
                                t.AddChild(currentNodeDir, fname, Color.Green);
                                FOUND = true;
                            }
                            else
                            {
                                t.AddChild(currentNodeDir, fname, Color.Red, updateColor: false);
                            }
                        }
                    }
                    
                    try
                    {
                        subDirs = System.IO.Directory.GetDirectories(currentDir);
                    }
                    catch (System.IO.DirectoryNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    if (FOUND && !allOccurence)
                    {
                        foreach (string dir in subDirs)
                        {
                            string dname = SplitPath(dir)[SplitPath(dir).Length - 1];
                            t.AddChild(currentNodeDir, dname, Color.Black, updateColor: false);
                        }
                    }
                    else
                    {
                        foreach (string dir in subDirs)
                        {

                            string dname = SplitPath(dir)[SplitPath(dir).Length - 1];
                            string childId = t.AddChild(currentNodeDir, dname, Color.Black, updateColor: false);
                            nodeDirQueue.Enqueue((childId, currentLevel+1));
                            dirQueue.Enqueue(dir);
                        }
                    }
                    
                }
            }
            if (Solution.Count == 0)
            {
                t.SetToRed(t.getID);
            }
            return FOUND;
        }
    }
}