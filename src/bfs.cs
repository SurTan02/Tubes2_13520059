using System;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using Color = Microsoft.Msagl.Drawing.Color;

namespace TubesStima2
{
    public class BreadthFirstSearch
    {
        public List<string> Solution;
        public BreadthFirstSearch()
        {
            Solution = new List<string>();
        }
        

        public void BFS(string root, string searchValue, bool allOccurrence, DrawingTree t)
        {
            Boolean found = false;
            Queue<string> dirQueue = new Queue<string>();
            Queue<(string, int)> nodeDirQueue = new Queue<(string, int)>();
            dirQueue.Enqueue(root);
            nodeDirQueue.Enqueue((t.RootId, 0));

            while (dirQueue.Count > 0)
            {
                string currentDir = dirQueue.Dequeue();
                (string, int) currentNode = nodeDirQueue.Dequeue();
                string currentNodeDir = currentNode.Item1;
                int currentLevel = currentNode.Item2;
                string[] files;
                string[] subDirs;
                
                t.SetColor(currentNodeDir, Color.Red);
                
                try {
                    files = Directory.GetFiles(currentDir);
                    subDirs = Directory.GetDirectories(currentDir);
                }
                catch (DirectoryNotFoundException e) {
                    MessageBox.Show(e.Message);
                    return;
                }
                
                // Kasus jika folder kosong, update menjadi warna merah
                if (files.Length == 0 && subDirs.Length == 0)
                { 
                    t.SetColor(currentNodeDir, Color.Red);
                }

                foreach (string fi in files)
                {
                    string fname = Path.GetFileName(fi);
                    if (found && !allOccurrence)
                    {
                        t.AddChild(currentNodeDir, fname, Color.Black);
                    }
                    else
                    {
                        if (fname == searchValue)
                        {
                            Solution.Add(fi);
                            t.AddChild(currentNodeDir, fname, Color.Green);
                            found = true;
                        }
                        else
                        {
                            t.AddChild(currentNodeDir, fname, Color.Red);
                        }
                    }
                }
                
                if (found && !allOccurrence)
                {
                    foreach (string dir in subDirs)
                    {
                        string dname = Path.GetFileName(dir);
                        t.AddChild(currentNodeDir, dname, Color.Black);      
                    }
                }
                else
                {
                    foreach (string dir in subDirs)
                    {
                        string dname = Path.GetFileName(dir);
                        string childId = t.AddChild(currentNodeDir, dname, Color.Black);
                        nodeDirQueue.Enqueue((childId, currentLevel+1));
                        dirQueue.Enqueue(dir);
                    }
                }

            }
        }
    }
}