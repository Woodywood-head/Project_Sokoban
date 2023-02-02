using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Runtime.Remoting.Activation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Project_Sokoban
{
    //Steven Woodhead, HND Software Development: OOP - Sokoban
    class PopulateGrid 
    {
        
        private GameAppPage window { get; set; }
        //private Wall wall { get ;set; }

        public PopulateGrid(GameAppPage window)                     // constructor to set the reference of window
        {
            this.window = window;                                   // setting the window reference
        }

       public int[,] wallArray = new int[,]                         // 2D array of integers to store the wall positions
            {
                { 0,0},{ 0,1},{ 0,2},{ 0,3},{ 0,4},{ 0,5},{ 0,6},{ 0,7},{ 0,8},{ 0,9} ,
           { 1,0},{ 1,1},{ 1,2},{ 1,3},{ 1,4},{ 1,5},{ 1,6},{ 1,7},{ 1,8},{ 1,9},
           { 2,0},{ 2,1},{ 2,4},{ 2,9},
           { 3,0},{ 3,1},{ 3,4},{ 3,5},{ 3,6},{ 3,9},
           { 4,0},{ 4,1},{ 4,2},{ 4,9},
           { 5,0},{ 5,1},{ 5,3},{ 5,8},{ 5,9},{ 5,2},
           { 6,0},{ 6,3},{ 6,5},{ 6,6},{ 6,8},{ 6,9},
           { 7,0},{ 7,9},
           { 8,0},{ 8,1},{ 8,7},{ 8,9},
           { 9,0},{ 9,1},{ 9,2},{ 9,3},{ 9,4},{ 9,5},{ 9,6},{ 9,7},{ 9,8},{ 9,9}
            };

        public void gridContent(string uriLocation, int row, int column)        // method to add content to the grid
        {
            // create the image object using the source file of the image
            Image img = new Image() { Source = new BitmapImage(new Uri(uriLocation, UriKind.Relative)) };
            
            // add the image object to the grid container, by default this
            // will be placed in first row and first column
            window.gameGrid.Children.Add(img);
            // position the image in the specified row and column
            Grid.SetRow(img, row);
            Grid.SetColumn(img, column);
        }
        
        
        public void gridLevel()                         // Method to create the level grid
        {
                            // Future implementation????: Wall wall = new Wall();
                            // Future implementation????: wall.addWall();

            switch (window.getLevel)                // switch statement to determine which level to display based on the getLevel value returned from the window object
            {
                case 1:
                    addWall();                      // call the addWall method to add wall objects to the grid
                    addBlank();                     // call the addBlank method to add blank spaces to the grid
                    addMan();                       // call the addMan method to add the player to the grid
                    addBox();                       // call the addBox method to add box objects to the grid
                    endPoint();                    // call the endPoint method to add the end point object to the grid
                    break;
                case 2:
                    window.MovesMade = -1;          // reset the moves made to -1
                    addWall();                      // call the addWall method to add walls to the grid
                    addBlank();                     // call the addBlank method to add blank spaces to the grid
                    addMan();                       // call the addMan method to add the player to the grid
                    endPoint();                     // call the endPoint method to add the end point object to the grid
                    addBox();                       // call the addBox method to add box objects to the grid
                    break;
                default: break;                     // if no level is found break. this will break before here. it will break in Movement
            }
            
        }

      public void addWall()                                                 // Method to add wall objects to the grid
        {
            for (int x = 0; x < wallArray.GetLength(0) ; x++)               
            {
                for (int y = 0; y < wallArray.GetLength(1); y++)            // loop through the 2D array of wall positions
                {
                    gridContent("Images\\wall.png", wallArray[x,0], wallArray[x,1]);    // call the gridContent method and pass the image location, row and column to add the wall object to the grid
                }
            }
        }
        
        public void addBlank()
        {
            //Add blank images too row 2
            gridContent("Images\\blank.png", 2, 2);
            gridContent("Images\\blank.png", 2, 3);
            gridContent("Images\\blank.png", 2, 5);
            gridContent("Images\\blank.png",2 ,6 );
            gridContent("Images\\blank.png", 2, 7);
            gridContent("Images\\blank.png", 2,8 );
            //Add blank images too row 3
            gridContent("Images\\blank.png", 3, 2);
            gridContent("Images\\blank.png", 3, 3);
            gridContent("Images\\blank.png", 3, 7);
            gridContent("Images\\blank.png", 3,8 );
            //Add blank images too row 4
            gridContent("Images\\blank.png", 4, 3);
            gridContent("Images\\blank.png", 4, 4);
            gridContent("Images\\blank.png", 4,6 );
            gridContent("Images\\blank.png", 4,7 );
            gridContent("Images\\blank.png", 4, 8);
            //Add blank images too row 5
            gridContent("Images\\blank.png", 5, 4);
            gridContent("Images\\blank.png", 5, 6);
            gridContent("Images\\blank.png", 5, 7);
            //Add blank images too row 6
            gridContent("Images\\blank.png",6 ,1 );
            gridContent("Images\\blank.png", 6, 2);
            gridContent("Images\\blank.png",6 , 7);
            gridContent("Images\\blank.png", 6, 4);
            //Add blank images too row 7
            gridContent("Images\\blank.png",7 ,1 );
            gridContent("Images\\blank.png" ,7,2 );
            gridContent("Images\\blank.png", 7, 3);
            gridContent("Images\\blank.png", 7,4 );
            gridContent("Images\\blank.png", 7,5 );
            gridContent("Images\\blank.png", 7,6 );
            gridContent("Images\\blank.png", 7, 7);
            gridContent("Images\\blank.png",7 , 8);
            //Add blank images too row 8
            gridContent("Images\\blank.png",8 ,2 );
            gridContent("Images\\blank.png", 8, 3);
            gridContent("Images\\blank.png", 8, 4);
            gridContent("Images\\blank.png",8 ,5 );
            gridContent("Images\\blank.png", 8, 6);
            gridContent("Images\\blank.png", 8, 8);
        }

        public void addMan()                                    // method to add the man to the grid
        {
            switch(window.getLevel)                             // This switch statement is used to determine which level the user is on
            {

                case 1:
                    gridContent("Images\\man1.png", 4, 4);       // If the level is 1, the image for the man to row 4 column 4
                    window.manRow = 4;                           // The row and column values are also stored in the class variables `manRow` and `manCol`
                    window.manCol = 4;
                    break;
                case 2:
                    gridContent("Images\\man1.png", 2, 8);      // If the level is 2, the image for the man to row 2 column 8
                    window.manRow = 2;                          // The row and column values are also stored in the class variables `manRow` and `manCol`
                    window.manCol = 8;
                    break;
                default: break;                                 // In case the level does not match either 1 or 2, the switch statement will break and not do anything     
            }
        }
       
        public void addBox()                                    // Method to add box to the grid depending on what the level is.
        {
            switch (window.getLevel)                            // This switch statement is used to determine which level the user is on
            {
                case 1:                                            // If the level is 1, the image for the box is placed at row 4 and column 5
                    gridContent("Images\\box1.png", 4, 5);          
                    window.boxRow = 4;                         // The row and column values are also stored in the class variables `boxRow` and `boxCol`
                    window.boxCol = 5;
                    break;
                case 2:                                             // If the level is 2, the image for the box is placed at row 5 and column 5
                    gridContent("Images\\box1.png", 5, 5);          
                    window.boxRow = 5;                         // The row and column values are also stored in the class variables `boxRow` and `boxCol`
                    window.boxCol = 5;
                    break;
                default: break;                                 // In case the level does not match either 1 or 2, the switch statement will break and not do anything
            }
        }
       
        public void endPoint()                               // Method to add end point to the grid depending on what the level is.
        {
            switch (window.getLevel)                        // This switch statement is used to determine which level the user is on
            {
                case 1:                                         // If the level is 1, the image for the box is placed at row 2 and column 5
                    gridContent("Images\\end.png", 2, 5);   
                    window.endPRow = 2;                      // The row and column values are also stored in the class variables `endPRow` and `endPCol`
                    window.endPCol = 5;
                    break;
                case 2:                                         // If the level is 1, the image for the box is placed at row 6 and column 2
                    gridContent("Images\\end.png", 6, 2);
                    window.endPRow = 6;                      // The row and column values are also stored in the class variables `endPRow` and `endPCol`
                    window.endPCol = 2;
                    break;
                default: break;                             // In case the level does not match either 1 or 2, the switch statement will break and not do anything
            }
        }
    }
}
