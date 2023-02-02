using System.Windows;
using System.Windows.Input;

namespace Project_Sokoban
{
    //Steven Woodhead, HND Software Development: OOP - Sokoban
    class Movement                                          // Class for handling movement in the Sokoban game
    {
        private GameAppPage window { get; set; }            // Property to store an instance of the GameAppPage class
        private PopulateGrid populateGrid { get; set; }     // Property to store an instance of the PopulateGrid class
        //***********//Future code                          //private Wall wall { get; set; }
        private int[,] wallArray;                           // Property to store an instance of the wallArray
        public int[,] WallArray                             // Public property to access and set the wallArray
        {
            get { return wallArray; }                       // Getter to retrieve the value of wallArray
            set { wallArray = value; }                      // Setter to set the value of wallArray.
        }
        public int getLevel { get; set; }                   // Public property to access and set the level of the game
        private int manTargetCellRow, manTargetCellCol;     // Variables to store the man target cell coordinates
        private int boxTargetCellRow, boxTargetCellCol;     // Variables to store the box target cell coordinates

        public Movement(GameAppPage window)                 // Constructor to initialize the Movement class and set the window property
        {
            this.window = window;                           // Set the passed in window instance to the private window property
        }
        public void MoveMan(KeyEventArgs e)                 // Method to move the man character in the Sokoban game based on the input key
        {
            switch (e.Key)                                  // Switch statement to determine which key was pressed
            {
                case Key.Left:                              // If the key is left, call the move method with "left" as a parameter
                    move("left");
                    break;
                case Key.Up:                                // If the key is up, call the move method with "up" as a parameter
                    move("up");
                    break;
                case Key.Right:                             // If the key is right, call the move method with "right" as a parameter
                    move("right");
                    break;
                case Key.Down:                              // If the key is down, call the move method with "down" as a parameter
                    move("down");
                    break;
                default:                                    // If the key is not one of the arrow keys, do nothing
                    break;
            }
        }

        private bool isWall()                               // Method to check if the target cell for the man or box is a wall
        {
            this.WallArray = populateGrid.wallArray;                // Set the wallArray property to the value of the wallArray property in the PopulateGrid class
            for (int x = 0; x < WallArray.GetLength(0); x++)        // Loop through the rows of the wallArray
            {
                for (int y = 0; y < WallArray.GetLength(1); y++)    // Loop through the columns of the wallArray
                {
                    if ((manTargetCellRow == WallArray[x, 0] && manTargetCellCol == WallArray[x, 1])
                        || (boxTargetCellRow == WallArray[x, 0] && boxTargetCellCol == WallArray[x, 1]))     // If the target cell for the man or box is equal to the current cell in the wallArray
                    {
                        return true;                        // Return true, indicating that the target cell is a wall
                    }
                }
            }
            return false;                                   // Return false, indicating that the target cell is not a wall
        }

        private bool isBox()                                // Function that checks if the man's next move is a box
        {
            if ((manTargetCellRow == window.boxRow) && (manTargetCellCol == window.boxCol))   // If the man's next move target cell is the current location of the box
            {
                return true;                                // Return true, indicating that the target cell is a box
            }
            updateBox1Location();                           // Call function to update the location of the box
            return false;                                   // Return false, indicating that the target cell is not a box
        }

        private void move(string direction)                 // Function that moves the man based on the direction passed as a parameter
        {
            int i = 0, j = 0;                               // Variables to store the change in man's row and column position
            switch (direction)                              // Switch statement to determine the direction of movement
            {
                case "left": i = 0; j = -1; break;          
                case "right": i = 0; j = 1; break;
                case "up": i = -1; j = 0; break;
                case "down": i = 1; j = 0; break;
                default: break;
            }

            boxTargetCellRow = window.boxRow;              // Store the target row for the box
            boxTargetCellCol = window.boxCol;              // Store the target column for the box

            if (((manTargetCellRow = window.manRow + i) < 10) && ((manTargetCellRow = window.manRow + i) >= 0)
                && ((manTargetCellCol = window.manCol + j) < 10) && ((manTargetCellCol = window.manCol + j) >= 0)) // Check if the man's next move is within the boundaries of the grid
            {
                populateGrid = new PopulateGrid(window);    // Instatiate a new object of the PopulateGrid class
                if (isWall())                               // Check if the man's next move is a wall
                {
                    return;                                 // If the man's next move is a wall, return without making any movement
                }

                else if (isBox())                           // Check if there is a box in the target cell
                {
                    this.WallArray = populateGrid.wallArray;    // Get the wall array from the PopulateGrid class

                    for (int x = 0; x < WallArray.GetLength(0); x++)    // Loop through the wall array to see if there is a wall in the target cell where the box will move to
                    {
                        for (int y = 0; y < WallArray.GetLength(1); y++)   
                        {
                            if (boxTargetCellRow + i == WallArray[x, 0] && boxTargetCellCol + j == WallArray[x, 1])      // If there is a wall in the target cell, return and don't move the box
                            {
                                return;
                            }
                        }
                    }
                    boxTargetCellRow = window.boxRow + i;  //set target cell row for box as its location plus direction
                    boxTargetCellCol = window.boxCol + j;  //set target cell column for box as its location plus direction
                    //draw a box in the new co-ordinates
                    populateGrid.gridContent("Images\\box1.png", boxTargetCellRow, boxTargetCellCol);
                    //update the original cell where the box was to be a blank cell
                    populateGrid.gridContent("Images\\blank.png", window.boxRow, window.boxCol);
                    //update the location of the box to these new co-ordinates
                    updateBox1Location();
                    //draw a man in the new co-ordinates
                    populateGrid.gridContent("Images\\man1.png", manTargetCellRow, manTargetCellCol);
                    //update the original cell where the man was to be a blank cell
                    populateGrid.gridContent("Images\\blank.png", window.manRow, window.manCol);
                    //update the location of the man to these new co-ordinates
                    updateManLocation();
                }
                else                // else draw man in blank space.
                {
                    //draw a man in the new co-ordinates
                    populateGrid.gridContent("Images\\man1.png", manTargetCellRow, manTargetCellCol);
                    //update the original cell where the man was to be a blank cell
                    populateGrid.gridContent("Images\\blank.png", window.manRow, window.manCol);
                    //update the location of the man to these new co-ordinates
                    updateManLocation();
                }
            }
            if (window.boxRow == window.endPRow && window.boxCol == window.endPCol)   // if box is in an end point level up
            {
                this.getLevel = window.getLevel;        // getLevel in this class is updated to the value in GameAppPage class
                nextLevel(getLevel);                    // Go to nextLevel method with the value of getLevel
            }
        }
        private void updateBox1Location()               // method for updating the lacation of the box
        {
            window.boxRow = boxTargetCellRow;          //the original location in GameAppPage is updated with new Row Cell
            window.boxCol = boxTargetCellCol;          //the original location in GameAppPage is updated with new Column Cell
        }

        private void updateManLocation()            // method for updating the lacation of the man
        {
            window.manRow = manTargetCellRow;       //the original location in GameAppPage is updated with new Row Cell
            window.manCol = manTargetCellCol;       //the original location in GameAppPage is updated with new Column Cell
        }

        private void nextLevel(int levelUP)                 // Method to move to next level
        {
            getLevel++;                                     // Increment the current level by 1

            levelUP = getLevel;                             // Store the incremented level in `levelUP` for use in switch statement
            window.getLevel = this.getLevel;                // Update the original instance of `getLevel` with the incremented value
            if (getLevel <= 2)                              // Check if the current level is less than or equal to 2
            {
                switch (levelUP)                            // Use the `levelUP` integer to determine which case to go to
                {
                    case 1: MessageBox.Show("You Completed Level: TEST", "TESTer TESTer"); populateGrid.gridLevel(); break;    // If on level 1 (left for testing purposes), show message and call gridLevel1 method from `populateGrid` class
                    case 2: MessageBox.Show("You Completed Level: 1", "Winner Winner"); populateGrid.gridLevel(); break;       // If on level 2, show message and call gridLevel1 method from `populateGrid` class
                    case 3: MessageBox.Show("Congratulations You Completed Sokoban"); break;                                    // If on level 3, show "Congratulations You Completed Sokoban" message
                    default: break;                         // If none of the above, do nothing
                }
            }
            else
            {
                MessageBox.Show("Congratulations You Completed Sokoban");            // If current level is higher than 2, show "Congratulations You Completed Sokoban" message
            }

        }
    }
}
