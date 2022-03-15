using System.Windows.Markup;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

namespace WpfApplication1 {
    enum Color {
        Blue, Red, Black
    }

    public class DrawingGraph {
        private GViewer viewer;
        private Graph graph;
        private static int nodeCount = 0;
        
        public DrawingGraph() {
            viewer = new GViewer();
            graph = new Graph();
        }

        public DrawingGraph(string name) {
            viewer = new GViewer();
            graph = new Graph();
            Node root = new Node(nodeCount.ToString());
            root.LabelText = name;
            graph.AddNode(name);
            nodeCount += 1;
        }

        public void AddChild(Node parent, string name) {
            Node child = new Node(nodeCount.ToString());
            child.LabelText = name;
            graph.AddNode(child);
            graph.AddEdge(parent.Id, child.Id);
            nodeCount += 1;
        }
        
        public void AddChild(string parentid, string childname) {
            Node child = new Node(nodeCount.ToString());
            Node parent = graph.FindNode(parentid);
            child.LabelText = childname;
            graph.AddNode(child);
            graph.AddEdge(parent.Id, child.Id);
            nodeCount += 1;
        }

        public void Display() {
            viewer.Graph = graph;
            GraphRenderer renderer = new GraphRenderer (graph);
            renderer.CalculateLayout();
            int width = 50;
            Bitmap bitmap = new Bitmap(width, (int)(graph.Height * 
                                                    (width / graph.Width)), PixelFormat.Format32bppPArgb); 
            renderer.Render(bitmap); 
            bitmap.Save("test.png");
            // Unfinished
        }
        
    }
}