using System;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;

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

            
            if (BfsButton.IsChecked == true) {
                // BFS(StartingDirectoryTextBlock.Text, FindAllCheck.IsChecked)
            }

            else {
                // DFS(StartingDirectoryTextBlock.Text, FindAllCheck.IsChecked)
            }
        }
    }
}