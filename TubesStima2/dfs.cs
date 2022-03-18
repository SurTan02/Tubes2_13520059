using System.IO;
using System.Collections.Generic;
using System;
using Color = Microsoft.Msagl.Drawing.Color;

namespace TubesStima2
{
    public class DepthFirstSearch
    {
        public List<string> Solution;
        public DepthFirstSearch(){
            this.Solution= new List<string>();
        }
        public Boolean DFS(string root, string searchValue, bool allOccurance,DrawingTree t, Boolean FOUND)
        {
            string[] files = null;
            string[] subDirs = null;
           
            // Memeriksa file didalam folder "root"
            try 
            {
                files = System.IO.Directory.GetFiles(root);

                //Kasus jika folder kosong, update menjadi warna merah
                if (files.Length == 0){
                    t.UpdateEmptyFolderColor(t.getID);
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
                    if (Path.GetFileName(fi) == searchValue){
                        Solution.Add(fi);
                        string LastName = SplitPath(fi)[SplitPath(fi).Length-1];
                        t.AddChild(LastName, Color.Green);
                        FOUND = true;

                        //Jika all Occurance tidak di cek
                        if (!allOccurance)   break;
                    }
                    else{
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
                    
                    string LastName = SplitPath(dirInfo)[SplitPath(dirInfo).Length-1];
                    DrawingTree t1 = new DrawingTree(LastName, Color.Black);
                    // Jika Sudah ditemukan (Kasus non allOccurance)maka subfolder tidak akan dicek
                    if(!FOUND || allOccurance){
                       FOUND =  DFS(dirInfo,searchValue,allOccurance,t1,FOUND);
                    }
                    
                    t.AddChild(t1);
                }
                
            }
            return FOUND;
            
        }
        //Fungsi untuk menghasilkan string independen dari direktori
        //C:\Tes >> C,TES
        public  String[] SplitPath(string path)
        {
                String[] pathSeparators = new String[] { "\\" };
                return path.Split(pathSeparators, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}

