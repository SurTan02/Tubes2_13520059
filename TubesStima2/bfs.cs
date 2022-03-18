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
            string[] files = null;
            string[] subDirs = null;
            // process all files directly under this folder
            try
            {
                files = System.IO.Directory.GetFiles(root);
                if (files.Length == 0)
                {
                    t.UpdateEmptyFolderColor(t.getID);
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
                        t.AddChild(LastName, Color.Green);
                        FOUND = true;

                        // Jika all Occurence tidak di cek
                        if (!allOccurence) break;
                    }
                    else
                    {
                        t.AddChild(Path.GetFileName(fi), Color.Red);
                    }
                }
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(root);
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }

                foreach (string dirInfo in subDirs)
                {
                    // this.QUEUE.Push(dirInfo)
                }
            }

            return FOUND;
        }
    }
}
