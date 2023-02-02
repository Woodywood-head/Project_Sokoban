using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Project_Sokoban.Properties;

namespace Project_Sokoban
{
    //Steven Woodhead, HND Software Development: OOP - Sokoban
    class GameAppPage : Window                          // GameAppPage class derived from the Window class
    {
        private Canvas canvas { get; set; }             // Property for the page canvas where everything will sit on
        private Button btnReset { get; set; }           // Property for the button to reset the game
        private Button btnHome { get; set; }            // Property for the button to go to the home page
        private TextBlock instructions { get; set; }    // Property for the text block with instructions for the game
        private TextBlock MoveCount { get; set; }       // Property for the TextBlock that displays the Move Count
        private TextBox Count { get; set; }             // Property for the TextBox to receive the number of steps taken
        private Border gameBorder { get; set; }         // Property for the border of the grid where the game will be
        public Grid gameGrid { get; set; }              // Property for the 10 by 10 game grid
        private PopulateGrid populateGrid { get; set; } // Property for the PopulateGrid class
                                                        // Property for future Wall class (commented out)
                                                        //private Wall wall { get; set; }
        public int MovesMade { get; set; }              // Property for the number of moves made
        public int getLevel { get; set; }               // Property for the level of the game
        public int manRow { get; set; }                 // Property for the row position of the man
        public int manCol { get; set; }                 // Property for the column position of the man
        public int[,] wallArry { get; set; }            // Property for the array representing the walls
        public int boxRow { get; set; }                 // Property for the row position of the box
        public int boxCol { get; set; }                 // Property for the column position of the box
        public int endPRow { get; set; }                // Property for the row position of the first endpoint
        public int endPCol { get; set; }                // Property for the column position of the first endpoint
        private Movement mover { get; set; }            // Property for the Movement class
        public GameAppPage(string Sokoban)              // Constructor for the GameAppPage class
        {
            this.Title = Sokoban;                       // Set the title of the page to "Sokoban"
            this.Height = 500;                          // Set the height of the page to 500
            this.Width = 850;                           // Set the width of the page to 850
            initializeWindow();                         // Call the initializeWindow method to set up the page
        }

        private void initializeWindow()                 // The method "initializeWindow" is responsible for setting up the game window.
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;  // The starting location of the game window is set to the center of the screen.
            canvas = new Canvas();                      // A new canvas is created.
            createGameLayout();                         // The game layout is created, including the grid where the game is played.
            createRightSideLayout();                    // The right side of the screen, which includes buttons and instructions, is set up.
            gameGrid.Focus();                           // The focus is set on the game grid.
            populateGrid = new PopulateGrid(this);      // An instance of the class "PopulateGrid" is created.
            //future                    // wall = new Wall();
            getLevel = 1;                               // The initial level of the game is set to 1.
            populateGrid.gridLevel();                   // The method "gridLevel" is called on the "populateGrid" instance
            this.Content = this.canvas;                 // The canvas is set as the content of the window.
            mover = new Movement(this);                 // An instance of the "Movement" class is created.
            setupPageEvents();                          // The method "setupPageEvents" is called.
        }

        private void createGameLayout()                 // The method "createGameLayout" is responsible for creating the game layout, including the game grid.
        {   
            // Border build
            gameBorder = new Border();                                  // A new border is created.
            gameBorder.BorderThickness = new Thickness(10.00);          // The thickness of the border is set to 10.00.
            gameBorder.BorderBrush = Brushes.Gray;                      // The color of the border is set to gray.
            // Grid Build
            gameGrid = new Grid();                                      // A new grid is created.
            gameGrid.Width = gameGrid.Height = 400;                     // The width and height of the grid are set to 400.
            gameGrid.HorizontalAlignment = HorizontalAlignment.Left;    // The horizontal alignment of the grid is set to left.
            gameGrid.VerticalAlignment = VerticalAlignment.Top;         // The vertical alignment of the grid is set to top.
            gameGrid.Focusable = true;                                  // The grid is set to be focusable.
            gameBorder.Child = gameGrid;                                // The grid is set as the child of the border.

            for (int i = 0; i < 10; i++)                                // 10 column definitions are added to the grid.
            {
                gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 10; i++)                                // 10 row definitions are added to the grid.
            {
                gameGrid.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void createRightSideLayout()
        {
            instructions = new TextBlock();                                     // Declare and initialize a TextBlock named "instructions"
            instructions.FontSize = 20;                                         // Set the font size of the "instructions" TextBlock to 20
            instructions.Text = "Use Arrow Keys To \nMove The Box Into Place";  // Set the text of the "instructions" TextBlock 
            instructions.VerticalAlignment = VerticalAlignment.Top;             // Set the vertical alignment of the "instructions" TextBlock to "Top"
            instructions.HorizontalAlignment = HorizontalAlignment.Right;       // Set the horizontal alignment of the "instructions" TextBlock to "Top"

            MoveCount = new TextBlock();                                        // Declare and initialize a TextBlock named "MoveCount"
            MoveCount.FontSize = 20;                                            // Set the font size of the "MoveCount" TextBlock to 20
            MoveCount.Text = "Move Counter";                                    // Set the text of the "MoveCount" TextBlock
            MoveCount.VerticalAlignment = VerticalAlignment.Center;             // Set the vertical alignment of the "MoveCount" TextBlock to "Center"
            MoveCount.HorizontalAlignment = HorizontalAlignment.Center;         // Set the horizontal alignment of the "MoveCount" TextBlock to "Center"
                
            Count = new TextBox();                                              // Declare and initialize a TextBox named "Count"
            Count.FontSize = 20;                                                // Set the font size of the "Count" TextBox to 20
            Count.Text = MovesMade.ToString();                                  // Set the text of the "Count" TextBox to the value of "MovesMade" converted to a string
            Count.VerticalAlignment = VerticalAlignment.Center;                 // Set the vertical alignment of the "Count" TextBox to "Center"
            Count.HorizontalAlignment = HorizontalAlignment.Center;             // Set the horizontal alignment of the "Count" TextBox to "Center"
            Count.Focusable = false;                                            // Set the "Count" TextBox to be non-focusable

            btnReset = new Button();                                            // Declare and initialize a Button named "btnReset"
            btnReset.Height = 50;                                               // Set the height of the "btnReset" button to 50
            btnReset.Width = 195;                                               // Set the width of the "btnReset" button to 195
            btnReset.FontSize = 15;                                             // Set the font size of the "btnReset" button to 15
            btnReset.Focusable = false;                                         // Set the "btnReset" button to be non-focusable
            btnReset.Content = "Restart Level";                                 // Set the content of the "btnReset" button

            btnHome = new Button();                                             // Declare and initialize a Button named "btnHome"
            btnHome.Height = 50;                                                // Set the height of the "btnHome" button to 50
            btnHome.Width = 195;                                                // Set the width of the "btnHome" button to 195
            btnHome.FontSize = 15;                                              // Set the font size of the "btnHome" button to 15
            btnHome.Focusable = false;                                          // Disable focus ability of the "Back Home" button
            btnHome.Content = "Back Home";                                      // Set the content of the "Back Home" button
            createCanvas();                                                     // Call the createCanvas method
        }

        private void createCanvas()                                             // Method to create the canvas
        {
            canvas.Children.Add(instructions);                                  // Add the element instructions to the canvas
            canvas.Children.Add(MoveCount);                                     // Add the element MoveCount to the canvas
            canvas.Children.Add(Count);                                         // Add the element Count to the canvas
            canvas.Children.Add(btnReset);                                      // Add the element btnReset to the canvas
            canvas.Children.Add(btnHome);                                       // Add the element btnHome to the canvas
            canvas.Children.Add(gameBorder);                                    // Add the element gameBorder to the canvas
            // Set the position of the instructions element to the right with a distance of 75 and top with a distance of 50
            Canvas.SetRight(instructions, 75);                                  
            Canvas.SetTop(instructions, 50);
            // Set the position of the MoveCount element to the right with a distance of 150 and top with a distance of 150
            Canvas.SetRight(MoveCount, 150);
            Canvas.SetTop(MoveCount, 150);
            // Set the position of the Count element to the right with a distance of 75 and top with a distance of 150
            Canvas.SetRight(Count, 75);
            Canvas.SetTop(Count, 150);
            // Set the position of the btnReset element to the left with a distance of 455 and bottom with a distance of 50
            Canvas.SetLeft(btnReset, 455);
            Canvas.SetBottom(btnReset, 50);
            // Set the position of the btnHome element to the right with a distance of 5 and bottom with a distance of 50
            Canvas.SetRight(btnHome, 5);
            Canvas.SetBottom(btnHome, 50);
        }

        protected void btnHome_Click(object sender, RoutedEventArgs e)      //Event handler for click event on "Back Home" button
        {
            MainWindow mainWindow = new MainWindow();                       //Create a new MainWindow object
            mainWindow.Show();                                              //Show the MainWindow object
            this.Close();                                                   //Close the current window
        }
        protected void btnReset_Click(object sender, RoutedEventArgs e)     //Event handler for click event on "Restart Level" button
        {
            GameAppPage gameLevel1 = new GameAppPage("Sokoban Level 1");    //Create a new GameAppPage object with level 1
            gameLevel1.Show();                                              //Show the GameAppPage object
            this.Close();                                                   //Close the current window
        }

        private void setupPageEvents()                                      //Method to setup the events for buttons and game grid
        {
            btnReset.Click += btnReset_Click;                               //Click event of "Restart Level" button to the btnReset_Click event handler
            btnHome.Click += btnHome_Click;                                 //Click event of "Back Home" button to the btnHome_Click event handler
            gameGrid.KeyDown += gameGrid_KeyDown;                           //KeyDown event of game grid to the gameGrid_KeyDown event handler
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)        //Placeholder method for reset button click event
        {
            throw new NotImplementedException();                             //Throw a NotImplementedException error  
        }

        protected void gameGrid_KeyDown(object sender, KeyEventArgs e)      //Event handler for KeyDown event on game grid
        {
            mover.MoveMan(e);                                               //Call the MoveMan method of mover object and pass the KeyEventArgs
            MovesMade++;       // ***to be Updated to only increment steps***   //Increment the moves made
            Count.Text = MovesMade.ToString();                              //Update the text in Count text box with number of moves made
        }
    }
}
