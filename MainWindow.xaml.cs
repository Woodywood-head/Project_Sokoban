using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_Sokoban
{
    //Steven Woodhead, HND Software Development: OOP - Sokoban
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window            // Initialize the MainWindow class
    {
        public MainWindow()             // Constructor for MainWindow class
        {
            InitializeComponent();      // Initialize the window
            btnStart.Focus();           // Set focus to the start button
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)            // Event handler for Start button click
        {
            GameAppPage gameLevel1 = new GameAppPage("Sokoban Level");           // Create new instance of GameAppPage with "Sokoban Level" title
            gameLevel1.Show();                                                   // Show the game level window
            this.Close();                                                        // Close the MainWindow
        }
        
    }
}
