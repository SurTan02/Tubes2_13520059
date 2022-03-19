using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.WindowsAPICodePack.Dialogs;
using Color = Microsoft.Msagl.Drawing.Color;
using System.Windows.Media;

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
            
            DrawingTree t = new DrawingTree(StartingDirectoryTextBlock.Text, Color.Black);
            List<string> filepaths = null;

            if (BfsButton.IsChecked == true) {
                // BFS(StartingDirectoryTextBlock.Text, FindAllCheck.IsChecked)
                BreadthFirstSearch BFSX = new BreadthFirstSearch();
                BFSX.BFS(StartingDirectoryTextBlock.Text, FileNameTextBox.Text, FindAllCheck.IsChecked == true, t);
                filepaths = BFSX.Solution;
            }

            else {
                DepthFirstSearch DFSX = new DepthFirstSearch();
                DFSX.DFS(StartingDirectoryTextBlock.Text, FileNameTextBox.Text, FindAllCheck.IsChecked == true, t,
                    false);
                filepaths = DFSX.Solution;
            }
            
            if (filepaths.Count == 0){
                MessageBox.Show("No matching files found.", "Result");
                // filepaths.Add("No matching files found.");
            }
            
            int counter = 0;
            OutputStackPanel.Children.RemoveRange(2, OutputStackPanel.Children.Count - 2);
            foreach (string solution in filepaths) {
                counter += 1;
                TextBlock tb = new TextBlock();
                tb.Text = $"{counter}.  ";
                Run r = new Run();
                r.Text = solution;
                
                Hyperlink h = new Hyperlink();
                h.Inlines.Add(r);
                
                h.NavigateUri = new Uri(solution.Remove(solution.LastIndexOf("\\")));
                h.RequestNavigate += (sender1, e1) =>
                    System.Diagnostics.Process.Start(e1.Uri.ToString());
                tb.Inlines.Add(h);
                tb.TextWrapping = TextWrapping.Wrap;
                tb.Margin = new Thickness(30,3,30,3);
                
                
                tb.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("AliceBlue");;
                OutputStackPanel.Children.Add(tb);
            }

            SearchTreeImage.Graph = t.Graph;
            
        }

        
    }

}