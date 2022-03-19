using System;
using System.Linq;
using Microsoft.Msagl.Drawing;
using Color = Microsoft.Msagl.Drawing.Color;
using System.Windows.Media;

namespace TubesStima2 {
    public class DrawingTree {
        private Graph graph;
        private string rootId;
        private static int nodeCount = 0;

        public Graph Graph
        {
            get { return graph; }
        }
      
        public string getID{
            get { return rootId; }
        }

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

        private void UpdateColor(string id) {
            Node node = graph.FindNode(id);
            Color color = node.Label.FontColor;
            Edge edge;
            
            if (color == Color.Yellow)
                return;
            
            while (node.InEdges.Any()) {
                edge = node.InEdges.First();
                node = graph.FindNode(edge.Source);
                edge.Attr.Color = color;
                if (node.Label.FontColor == Color.Green) {
                    break;
                }
                node.Label.FontColor = color;
            }
            
        }

        public void UpdateEmptyFolderColor(string id) {
            Node node = graph.FindNode(id);
            node.Label.FontColor = Color.Red;
            }
            
        }
        
        
    
    
}