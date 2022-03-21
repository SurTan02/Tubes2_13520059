﻿using System.IO;
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
            Queue<string> dirQueue = new Queue<string>();
            Queue<string> fileQueue = new Queue<string>();
            Queue<(string, string)> nodeDrawQueue = new Queue<(string, string)>();
            Queue<string> nodeFileQueue = new Queue<string>();
            Queue<string> nodeDirQueue = new Queue<string>();
            dirQueue.Enqueue(root);
            nodeDirQueue.Enqueue(t.getID);
            // Queue<DrawingTree> dirNode = new Queue<DrawingTree>();
            string[] files = null;
            string[] subDirs = null;
            while (dirQueue.Count > 0 && (!FOUND || allOccurence))
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
                        Console.WriteLine(fi);
                        if (Path.GetFileName(fi) == searchValue)
                        {
                            Solution.Add(fi);
                            string LastName = SplitPath(fi)[SplitPath(fi).Length - 1];
                            string newNodeId = t.AddChild(dirNodeId, LastName, Color.Black);
                            nodeFileQueue.Enqueue(newNodeId);
                        }
                        try
                        {
                            subDirs = System.IO.Directory.GetDirectories(currDir);
                        }
                        catch (System.IO.DirectoryNotFoundException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        foreach (string dir in subDirs)
                        {
                            dirQueue.Enqueue(dir);
                            string LastName = SplitPath(dir)[SplitPath(dir).Length - 1];
                            nodeDrawQueue.Enqueue((dirNodeId, LastName));
                        }
                    }
                }

                while (nodeDrawQueue.Count > 0)
                {
                    (string, string) nodeProps = nodeDrawQueue.Dequeue();
                    string parentNodeId = nodeProps.Item1;
                    string nodeLabel = nodeProps.Item2;
                    string newNodeId = t.AddChild(parentNodeId, nodeLabel, Color.Black);
                    nodeDirQueue.Enqueue(newNodeId);
                }
                
                if (files != null)
                {
                    
                    while (nodeFileQueue.Count > 0 && fileQueue.Count > 0)
                    {
                        string currNode = nodeFileQueue.Dequeue();
                        string currFile = fileQueue.Dequeue();
                        if (Path.GetFileName(currFile) == searchValue)
                        {
                            Solution.Add(currFile);
                            t.SetToGreen(currNode);
                            FOUND = true;
                            if (!allOccurence) break;
                        }
                        else
                        {
                            t.SetToRed(currNode);
                        }
                    }
                }
            }
            return FOUND;
        }
    }
}