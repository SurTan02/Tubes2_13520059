using System.Windows.Markup;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Media.Imaging;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Color = Microsoft.Msagl.Drawing.Color;

namespace TubesStima2 {
    public class DrawingTree {
        private Graph graph;
        private string rootId;
        private static int nodeCount = 0;

        public DrawingTree() {
            graph = new Graph();
        }

        public DrawingTree(string name, Color color) {
            graph = new Graph();
            Node root = new Node(nodeCount.ToString());
            root.LabelText = name;
            root.Label.FontColor = color;
            rootId = root.Id;
            graph.AddNode(root);
            nodeCount += 1;
        }
        
        public string AddChild(string childname, Color color) {
            return AddChild(rootId, childname, color);
        }

        public void AddChild(DrawingTree child) {
            AddChild(rootId, child);
        }

        public string AddChild(string parentid, string childname, Color color) {
            if (graph.FindNode(parentid) == null) {
                throw new Exception("Parent ID not found.");
            }
            
            Node child = new Node(nodeCount.ToString());
            child.LabelText = childname;
            child.Label.FontColor = color;
            graph.AddNode(child); 
            
            Edge e = graph.AddEdge(parentid, child.Id);
            e.Attr.ArrowheadAtTarget = ArrowStyle.Normal;

            UpdateColor(child.Id);
            
            nodeCount += 1;
            return child.Id;
        }

        public void AddChild(string parentid, DrawingTree child) {
            if (graph.FindNode(parentid) == null) {
                throw new Exception("Parent ID not found.");
            }
            
            foreach (var childNode in child.graph.Nodes) {
                graph.AddNode(childNode);
            }

            Edge e = graph.AddEdge(parentid, child.rootId);
            e.Attr.ArrowheadAtTarget = ArrowStyle.Normal;
            
            UpdateColor(child.rootId);
        }

        public void UpdateColor(string id) {
            Node node = graph.FindNode(id);
            Color color = node.Label.FontColor;
            Edge edge;
            
            if (color == Color.Black)
                return;
            
            while (node.InEdges.Any()) {
                edge = node.InEdges.First();
                node = graph.FindNode(edge.Source);
                edge.Attr.Color = color;
                if (node.Label.FontColor == Color.Blue) {
                    break;
                }
                node.Label.FontColor = color;
            }
            
        }

        public BitmapImage Display() {
            GraphRenderer renderer = new GraphRenderer (graph);
            renderer.CalculateLayout();
            int width = 500;
            Bitmap bitmap = new Bitmap(width, (int)(graph.Height * (width / graph.Width)), 
                PixelFormat.Format32bppPArgb); 
            renderer.Render(bitmap);
            return Bmp2BmpImg(bitmap);
        }
        
        private static BitmapImage Bmp2BmpImg(Bitmap bmp) {
            using (var memory = new System.IO.MemoryStream()) {
                bmp.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

    }
    
}