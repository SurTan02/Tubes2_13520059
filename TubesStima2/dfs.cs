using System;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using Color = Microsoft.Msagl.Drawing.Color;

namespace TubesStima2
{
    public class DepthFirstSearch
    {
        public List<string> Solution;
        
        public DepthFirstSearch() 
        {
            Solution = new List<string>();
        }
        
        public Boolean DFS(string root, string searchValue, bool allOccurrence, DrawingTree t, Boolean found) {
            string[] files;
            string[] subDirs;
            string subDirName, fileName;
            int idx;
            
            // Memeriksa file didalam folder "root"
            try {
                files = Directory.GetFiles(root);
                subDirs = Directory.GetDirectories(root);
            }
            catch (DirectoryNotFoundException e) {
                MessageBox.Show(e.Message);
                return false;
            }
            
            //Kasus jika folder kosong, update menjadi warna merah
            if (files.Length == 0 && subDirs.Length == 0) {
                t.SetColor(Color.Red);
            }

            idx = 0;
            foreach (string fi in files) {
                idx++;
                fileName = Path.GetFileName(fi);
                if (fileName == searchValue) {

                    Solution.Add(fi);
                    t.AddChild(fileName, Color.Green);
                    found = true;

                    // Jika allOccurrence tidak di cek
                    if (!allOccurrence) {
                        for (int i = idx; i < files.Length; i++) {
                            t.AddChild(Path.GetFileName(files[i]), Color.Black);
                        }

                        break;
                    }
                }
                else {
                    t.AddChild(fileName, Color.Red);
                }
            }

            foreach (string dirInfo in subDirs) {
                subDirName = Path.GetFileName(dirInfo);
                DrawingTree t1 = new DrawingTree(subDirName, Color.Black);
                // Jika Sudah ditemukan (Kasus non allOccurrence)maka subfolder tidak akan dicek
                if (!found || allOccurrence) {
                    found = DFS(dirInfo, searchValue, allOccurrence, t1, found);
                }

                t.AddChild(t1);
            }
            
            return found;

        }
    }
}

