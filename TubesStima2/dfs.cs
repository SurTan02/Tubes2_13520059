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
        
        // Fungsi untuk menghasilkan string independen dari direktori
        // C:\Tes >> {C,TES}
        private String[] SplitPath(string path)
        {
            String[] pathSeparators = { "\\" };
            return path.Split(pathSeparators, StringSplitOptions.RemoveEmptyEntries);
        }

        public Boolean DFS(string root, string searchValue, bool allOccurance, DrawingTree t, Boolean found) {
            string[] files;
            string[] subDirs;
            string lastName;
            
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

            int idx = 0;
            foreach (string fi in files) {
                idx++;
                if (Path.GetFileName(fi) == searchValue) {

                    Solution.Add(fi);
                    lastName = SplitPath(fi)[SplitPath(fi).Length - 1];
                    t.AddChild(lastName, Color.Green);
                    found = true;

                    // Jika all Occurance tidak di cek
                    if (!allOccurance) {
                        for (int i = idx; i < files.Length; i++) {
                            t.AddChild(Path.GetFileName(files[i]), Color.Black);
                        }

                        break;
                    }
                }
                else {
                    t.AddChild(Path.GetFileName(fi), Color.Red);
                }
            }

            foreach (string dirInfo in subDirs) {
                lastName = SplitPath(dirInfo)[SplitPath(dirInfo).Length - 1];
                DrawingTree t1 = new DrawingTree(lastName, Color.Black);
                // Jika Sudah ditemukan (Kasus non allOccurance)maka subfolder tidak akan dicek
                if (!found || allOccurance) {
                    found = DFS(dirInfo, searchValue, allOccurance, t1, found);
                }

                t.AddChild(t1);
            }
            
            return found;

        }
    }
}

