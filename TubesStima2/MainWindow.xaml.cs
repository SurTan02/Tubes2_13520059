using System.Diagnostics;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using Color = Microsoft.Msagl.Drawing.Color;

namespace TubesStima2 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
        }

        private void StartingDirectoryButton_OnClick(object sender, RoutedEventArgs e) {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
                StartingDirectoryTextBlock.Text = dialog.FileName;
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e) {
            if (StartingDirectoryTextBlock.Text == "") {
                MessageBox.Show("Please choose the starting directory.", "Incomplete settings");
                return;
            }

            FileNameTextBox.Text = FileNameTextBox.Text.Trim();
            if (FileNameTextBox.Text == "") {
                MessageBox.Show("Please fill in the file name to be searched up.", 
                    "Incomplete settings");
                return;
            }
            
            // Cant use IsChecked as condition because type is System<Nullable> bool
            if (BfsButton.IsChecked == false && DfsButton.IsChecked == false) {
                MessageBox.Show("Please choose the search method.", "Incomplete settings");
                return;
            }
            
            // Example of DrawingTree

            DrawingTree t = new DrawingTree("root", Color.Black);
            string id1 = t.AddChild("a", Color.Aqua);
            t.AddChild(id1, "b", Color.Chocolate);
            t.AddChild(id1, "c", Color.Blue);
            string id2 = t.AddChild("α", Color.Indigo);
            t.AddChild(id2, "β", Color.Olive);

            DrawingTree t1 = new DrawingTree("γ", Color.Teal);
            t1.AddChild("δ", Color.IndianRed);
            t.AddChild(id2, t1, Color.Violet);
            SearchTreeImage.Source = t.Display();


            if (BfsButton.IsChecked == true) {
                // BFS(StartingDirectoryTextBlock.Text, FindAllCheck.IsChecked)
            }

            else {
                // DFS(StartingDirectoryTextBlock.Text, FindAllCheck.IsChecked)
            }
        }
    }
}