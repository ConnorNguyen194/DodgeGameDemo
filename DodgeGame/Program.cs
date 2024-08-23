using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodgeGame
{
    //The abstract class "gridGame" creates the bases for the other classes for each diffculty.
    abstract class gridGame
    {
        //Public variables to allows the child classes to use them.
        public string landSpace = "█";
        public string emptySpace = "░";
        public string player = "1";
        public string enemy = "2";
        public string warningSpace = "+";
        public string dangerSpace = "X";
        public int score = 0;
        public int rowUse = 0;
        public int columnUse = 0;
        public bool endingBool = false;

        //Abstract methods
        public abstract void displayGrid();
        public abstract void playerLand();
        public abstract void playerControl();
        public abstract void gridEditWS(int rowNum);
        public abstract void gridEditAD(int columnNum);
        public abstract void addDanger();
        public abstract void startGame();
        public abstract void scoreGain(int scoreInput);
        public abstract int finalScore();
    }

    //Child class for a easy difficulty.
    class easyGrid : gridGame
    {
        string[,] gridPlane = new string[7, 7];

        //Constructor that creates the grid.
        public easyGrid()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    gridPlane[i, j] = landSpace;
                }
            }
        }

        //Overridden methods from the abstract methods.

        //Prints the grid.
        public override void displayGrid()
        {
            //This for loop creates the grid's basis.
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Console.Write(gridPlane[i, j]);
                }
                Console.WriteLine();
            }
        }

        //Places the player at it's starting location.
        public override void playerLand()
        {
            gridPlane[0, 3] = player;
        }

        //The playerControl method takes in the wasd input to control the player.
        public override void playerControl()
        {
            string wasdSwitch;

            //The do loop continuously runs the code to move the player across the grid.
            do
            {
                Console.WriteLine("Score: " + score);
                Console.WriteLine("Type in w, a, s, d");
                wasdSwitch = Console.ReadLine();

                switch (wasdSwitch)
                {
                    case "w":
                        gridEditWS(-1);
                        addDanger();
                        displayGrid();
                        break;
                    case "a":
                        gridEditAD(-1);
                        addDanger();
                        displayGrid();
                        break;
                    case "s":
                        gridEditWS(1);
                        addDanger();
                        displayGrid();
                        break;
                    case "d":
                        gridEditAD(1);
                        addDanger();
                        displayGrid();
                        break;
                    default:
                        Console.WriteLine("Only type in w, a, s, or d");
                        break;
                }
            } while ((wasdSwitch != "w" || wasdSwitch != "a" || wasdSwitch != "s" || wasdSwitch != "d") && endingBool == false);

        }

        //A method that takes in w and s inputs for up and down.
        public override void gridEditWS(int rowNum)
        {
            int playerRow = 0, playerColumn = 0;
            string playerHold = "";
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (gridPlane[i,j] == player)
                    {
                        playerHold = player;
                        playerRow = i;
                        playerColumn = j;
                        goto PositionPlayer;
                    }
                }
            }

            //Breaks the nested for loop using the goto variable.
        PositionPlayer:

            rowUse = playerRow;
            columnUse = playerColumn;

            if ((playerRow + rowNum) <= 0)
            {
                if (gridPlane[0, playerColumn] == dangerSpace)
                {
                    endingBool = true;
                    Console.WriteLine("Game Over");
                }
                gridPlane[playerRow, playerColumn] = landSpace;
                gridPlane[0, playerColumn] = playerHold;
            }
            else if ((playerRow + rowNum) >= 6)
            {
                if (gridPlane[6, playerColumn] == dangerSpace)
                {
                    endingBool = true;
                    Console.WriteLine("Game Over");
                }
                gridPlane[playerRow, playerColumn] = landSpace;
                gridPlane[6, playerColumn] = playerHold;
            }
            else
            {
                if (gridPlane[playerRow + rowNum, playerColumn] == dangerSpace)
                {
                    endingBool = true;
                    Console.WriteLine("Game Over");
                }
                gridPlane[playerRow, playerColumn] = landSpace;
                gridPlane[playerRow + rowNum, playerColumn] = playerHold;
                scoreGain(10);
            }
        }

        //A method that takes in a and d inputs for left and right.
        public override void gridEditAD(int columnNum)
        {
            int playerRow = 0, playerColumn = 0;
            string playerHold = "";
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (gridPlane[i, j] == player)
                    {
                        playerHold = player;
                        playerRow = i;
                        playerColumn = j;
                        goto PositionPlayer;
                    }
                }
            }

        //Breaks the nested for loop using the goto variable.
        PositionPlayer:

            rowUse = playerRow;
            columnUse = playerColumn;

            if ((playerColumn + columnNum) <= 0)
            {
                if (gridPlane[playerRow, 0] == dangerSpace)
                {
                    endingBool = true;
                    Console.WriteLine("Game Over");
                }
                gridPlane[playerRow, playerColumn] = landSpace;
                gridPlane[playerRow, 0] = playerHold;
            }
            else if ((playerColumn + columnNum) >= 6)
            {
                if (gridPlane[playerRow, 6] == dangerSpace)
                {
                    endingBool = true;
                    Console.WriteLine("Game Over");
                }
                gridPlane[playerRow, playerColumn] = landSpace;
                gridPlane[playerRow, 6] = playerHold;
            }
            else
            {
                if (gridPlane[playerRow, playerColumn + columnNum] == dangerSpace)
                {
                    endingBool = true;
                    Console.WriteLine("Game Over");
                }
                gridPlane[playerRow, playerColumn] = landSpace;
                gridPlane[playerRow, playerColumn + columnNum] = playerHold;
                scoreGain(10);
            }
        }

        //A method that edits the gridPlane array.
        public override void addDanger()
        {
            Random rnd = new Random();
            int editToDanger;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (gridPlane[i,j] == landSpace)
                    {
                        editToDanger = rnd.Next(1,100);

                        if (editToDanger == 1 || editToDanger <= 5)
                        {
                            gridPlane[i, j] = warningSpace;
                        }
                    }
                    else if (gridPlane[i, j] == warningSpace)
                    {
                        gridPlane[i, j] = dangerSpace;
                    }
                    else if (gridPlane[i, j] == dangerSpace)
                    {
                        gridPlane[i, j] = landSpace;
                    }
                }
            }
        }

        //A method to start the game.
        public override void startGame()
        {
            while (endingBool == false)
            {
                if (endingBool == true)
                {
                    break;
                }
                else
                {
                    playerControl();
                }
            }
        }

        //A method to accumulate the score through gameplay.
        public override void scoreGain(int scoreInput)
        {
            score = score + scoreInput;
        }

        //A method to return the score when commanded.
        public override int finalScore()
        {
            return score;
        }
    }

    //Child class for a medium difficulty.
    class mediumGrid : gridGame
    {
        string[,] gridPlane = new string[7, 7];

        //Constructor that creates the grid.
        public mediumGrid()
        {
            //This for loop creates the grid's basis.
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    gridPlane[i, j] = landSpace;
                }
            }
            //This for loop creates the emptySpace text.
            for (int i = 0; i < 7; i++)
            {
                if ((i == 0) || (i == 6))
                {
                    for (int j = 0; j < 7; j++)
                    {
                        gridPlane[i, j] = emptySpace;
                    }
                }
                else if ((i > 0) && (i < 6))
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if ((j == 0) || (j == 6))
                        {
                            gridPlane[i, j] = emptySpace;
                        }
                        else if ((j > 0) && (j < 6))
                        {
                            gridPlane[i, j] = landSpace;
                        }
                    }
                }
            }
        }

        //Overridden methods from the abstract methods.

        //Prints the grid.
        public override void displayGrid()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Console.Write(gridPlane[i, j]);
                }
                Console.WriteLine();
            }
        }

        //Places the player at it's starting location.
        public override void playerLand()
        {
            gridPlane[1, 3] = player;
        }

        //The playerControl method takes in the wasd input to control the player.
        public override void playerControl()
        {
            string wasdSwitch;

            //The do loop continuously runs the code to move the player across the grid.
            do
            {
                Console.WriteLine("Score: " + score);
                Console.WriteLine("Type in w, a, s, d");
                wasdSwitch = Console.ReadLine();

                switch (wasdSwitch)
                {
                    case "w":
                        gridEditWS(-1);
                        addDanger();
                        displayGrid();
                        break;
                    case "a":
                        gridEditAD(-1);
                        addDanger();
                        displayGrid();
                        break;
                    case "s":
                        gridEditWS(1);
                        addDanger();
                        displayGrid();
                        break;
                    case "d":
                        gridEditAD(1);
                        addDanger();
                        displayGrid();
                        break;
                    default:
                        Console.WriteLine("Only type in w, a, s, or d");
                        break;
                }
            } while ((wasdSwitch != "w" || wasdSwitch != "a" || wasdSwitch != "s" || wasdSwitch != "d") && endingBool == false);

        }

        //A method that takes in w and s inputs for up and down.
        public override void gridEditWS(int rowNum)
        {
            int playerRow = 0, playerColumn = 0;
            string playerHold = "";
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (gridPlane[i, j] == player)
                    {
                        playerHold = player;
                        playerRow = i;
                        playerColumn = j;
                        goto PositionPlayer;
                    }
                }
            }

        //Breaks the nested for loop using the goto variable.
        PositionPlayer:

            rowUse = playerRow;
            columnUse = playerColumn;

            if ((playerRow + rowNum) <= 0)
            {
                if (gridPlane[playerRow + rowNum, playerColumn] == emptySpace)
                {
                    Console.WriteLine("Game Over");
                    endingBool = true;
                }
                gridPlane[0, playerColumn] = playerHold;
            }
            else if ((playerRow + rowNum) >= 6)
            {
                if (gridPlane[playerRow + rowNum, playerColumn] == emptySpace)
                {
                    Console.WriteLine("Game Over");
                    endingBool = true;
                }
                gridPlane[6, playerColumn] = playerHold;
            }
            else
            {
                if (gridPlane[playerRow + rowNum, playerColumn] == dangerSpace)
                {
                    Console.WriteLine("Game Over");
                    endingBool = true;
                }
                else if (gridPlane[playerRow + rowNum, playerColumn] == emptySpace)
                {
                    Console.WriteLine("Game Over");
                    endingBool = true;
                }
                gridPlane[playerRow, playerColumn] = landSpace;
                gridPlane[playerRow + rowNum, playerColumn] = playerHold;
                scoreGain(10);
            }
        }

        //A method that takes in a and d inputs for left and right.
        public override void gridEditAD(int columnNum)
        {
            int playerRow = 0, playerColumn = 0;
            string playerHold = "";
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (gridPlane[i, j] == player)
                    {
                        playerHold = player;
                        playerRow = i;
                        playerColumn = j;
                        goto PositionPlayer;
                    }
                }
            }

        //Breaks the nested for loop using the goto variable.
        PositionPlayer:

            rowUse = playerRow;
            columnUse = playerColumn;

            if ((playerColumn + columnNum) <= 0)
            {
                if (gridPlane[playerRow, playerColumn + columnNum] == emptySpace)
                {
                    endingBool = true;
                    Console.WriteLine("Game Over");
                }
                gridPlane[playerColumn, 0] = playerHold;
            }
            else if ((playerColumn + columnNum) >= 6)
            {
                if (gridPlane[playerRow, playerColumn + columnNum] == emptySpace)
                {
                    endingBool = true;
                    Console.WriteLine("Game Over");
                }
                gridPlane[playerColumn, 6] = playerHold;
            }
            else
            {
                if (gridPlane[playerRow, playerColumn + columnNum] == dangerSpace)
                {
                    endingBool = true;
                    Console.WriteLine("Game Over");
                }
                else if (gridPlane[playerRow, playerColumn + columnNum] == emptySpace)
                {
                    endingBool = true;
                    Console.WriteLine("Game Over");
                }
                gridPlane[playerRow, playerColumn] = landSpace;
                gridPlane[playerRow, playerColumn + columnNum] = playerHold;
                scoreGain(10);
            }
        }

        //A method that edits the gridPlane array.
        public override void addDanger()
        {
            Random rnd = new Random();
            int editToDanger;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (gridPlane[i, j] == landSpace)
                    {
                        editToDanger = rnd.Next(1, 100);

                        if (editToDanger == 1 || editToDanger <= 10)
                        {
                            gridPlane[i, j] = warningSpace;
                        }
                    }
                    else if (gridPlane[i, j] == warningSpace)
                    {
                        gridPlane[i, j] = dangerSpace;
                    }
                    else if (gridPlane[i, j] == dangerSpace)
                    {
                        gridPlane[i, j] = landSpace;
                    }
                }
            }
        }

        //A method to start the game.
        public override void startGame()
        {
            while (endingBool == false)
            {
                if (endingBool == true)
                {
                    break;
                }
                else
                {
                    playerControl();
                }
            }
        }

        //A method to accumulate the score through gameplay.
        public override void scoreGain(int scoreInput)
        {
            score = score + scoreInput;
        }

        //A method to return the score when commanded.
        public override int finalScore()
        {
            return score;
        }
    }

    //Child class for a hard difficulty.
    class hardGrid : gridGame
    {
        string[,] gridPlane = new string[8, 8];

        //Constructor that creates the grid.
        public hardGrid()
        {
            //This for loop creates the grid's basis.
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    gridPlane[i, j] = landSpace;
                }
            }
            //This for loop creates the emptySpace text.
            for (int i = 0; i < 8; i++)
            {
                if (((i >= 0) && (i < 2)) || ((i <= 7) && (i > 5)))
                {
                    for (int j = 0; j < 8; j++)
                    {
                        gridPlane[i, j] = emptySpace;
                    }
                }
                else if ((i >= 2) && (i <= 5))
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (((j >= 0) && (j < 2)) || ((j <= 7) && (j > 5)))
                        {
                            gridPlane[i, j] = emptySpace;
                        }
                        else if ((j >= 2) && (j <= 5))
                        {
                            gridPlane[i, j] = landSpace;
                        }
                    }
                }
            }
        }

        //Overridden methods from the abstract methods.

        //Prints the grid.
        public override void displayGrid()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(gridPlane[i, j]);
                }
                Console.WriteLine();
            }
        }

        //Places the player at it's starting location.
        public override void playerLand()
        {
            gridPlane[2, 2] = player;
        }

        //The playerControl method takes in the wasd input to control the player.
        public override void playerControl()
        {
            string wasdSwitch;

            //The do loop continuously runs the code to move the player across the grid.
            do
            {
                Console.WriteLine("Score: " + score);
                Console.WriteLine("Type in w, a, s, d");
                wasdSwitch = Console.ReadLine();

                switch (wasdSwitch)
                {
                    case "w":
                        gridEditWS(-1);
                        addDanger();
                        displayGrid();
                        break;
                    case "a":
                        gridEditAD(-1);
                        addDanger();
                        displayGrid();
                        break;
                    case "s":
                        gridEditWS(1);
                        addDanger();
                        displayGrid();
                        break;
                    case "d":
                        gridEditAD(1);
                        addDanger();
                        displayGrid();
                        break;
                    default:
                        Console.WriteLine("Only type in w, a, s, or d");
                        break;
                }
            } while ((wasdSwitch != "w" || wasdSwitch != "a" || wasdSwitch != "s" || wasdSwitch != "d") && endingBool == false);

        }

        //A method that takes in w and s inputs for up and down.
        public override void gridEditWS(int rowNum)
        {
            int playerRow = 0, playerColumn = 0;
            string playerHold = "";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (gridPlane[i, j] == player)
                    {
                        playerHold = player;
                        playerRow = i;
                        playerColumn = j;
                        goto PositionPlayer;
                    }
                }
            }

        //Breaks the nested for loop using the goto variable.
        PositionPlayer:

            rowUse = playerRow;
            columnUse = playerColumn;

            if ((playerRow + rowNum) <= 0)
            {
                if (gridPlane[playerRow + rowNum, playerColumn] == emptySpace)
                {
                    Console.WriteLine("Game Over");
                    endingBool = true;
                }
                gridPlane[0, playerColumn] = playerHold;
            }
            else if ((playerRow + rowNum) >= 7)
            {
                if (gridPlane[playerRow + rowNum, playerColumn] == emptySpace)
                {
                    Console.WriteLine("Game Over");
                    endingBool = true;
                }
                gridPlane[7, playerColumn] = playerHold;
            }
            else
            {
                if (gridPlane[playerRow + rowNum, playerColumn] == dangerSpace)
                {
                    Console.WriteLine("Game Over");
                    endingBool = true;
                }
                else if (gridPlane[playerRow + rowNum, playerColumn] == emptySpace)
                {
                    Console.WriteLine("Game Over");
                    endingBool = true;
                }
                gridPlane[playerRow, playerColumn] = landSpace;
                gridPlane[playerRow + rowNum, playerColumn] = playerHold;
                scoreGain(10);
            }
        }

        //A method that takes in a and d inputs for left and right.
        public override void gridEditAD(int columnNum)
        {
            int playerRow = 0, playerColumn = 0;
            string playerHold = "";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (gridPlane[i, j] == player)
                    {
                        playerHold = player;
                        playerRow = i;
                        playerColumn = j;
                        goto PositionPlayer;
                    }
                }
            }

        //Breaks the nested for loop using the goto variable.
        PositionPlayer:

            rowUse = playerRow;
            columnUse = playerColumn;

            if ((playerColumn + columnNum) <= 0)
            {
                if (gridPlane[playerRow, playerColumn + columnNum] == emptySpace)
                {
                    Console.WriteLine("Game Over");
                    endingBool = true;
                }
                gridPlane[playerColumn, 0] = playerHold;
            }
            else if ((playerColumn + columnNum) >= 7)
            {
                if (gridPlane[playerRow, playerColumn + columnNum] == emptySpace)
                {
                    Console.WriteLine("Game Over");
                    endingBool = true;
                }
                gridPlane[playerColumn, 7] = playerHold;
            }
            else
            {
                if (gridPlane[playerRow, playerColumn + columnNum] == dangerSpace)
                {
                    Console.WriteLine("Game Over");
                    endingBool = true;
                }
                else if (gridPlane[playerRow, playerColumn + columnNum] == emptySpace)
                {
                    Console.WriteLine("Game Over");
                    endingBool = true;
                }
                gridPlane[playerRow, playerColumn] = landSpace;
                gridPlane[playerRow, playerColumn + columnNum] = playerHold;
                scoreGain(10);
            }
        }

        //A method that edits the gridPlane array.
        public override void addDanger()
        {
            Random rnd = new Random();
            int editToDanger, editToLand;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (gridPlane[i, j] == landSpace)
                    {
                        editToDanger = rnd.Next(1, 100);

                        if (editToDanger == 1 || editToDanger <= 15)
                        {
                            gridPlane[i, j] = warningSpace;
                        }
                        else if (editToDanger == 16 || editToDanger <= 25)
                        {
                            gridPlane[i, j] = emptySpace;
                        }
                    }
                    else if (gridPlane[i, j] == warningSpace)
                    {
                        gridPlane[i, j] = dangerSpace;
                    }
                    else if (gridPlane[i, j] == dangerSpace)
                    {
                        gridPlane[i, j] = landSpace;
                    }
                    else if (gridPlane[i, j] == emptySpace)
                    {
                        editToLand = rnd.Next(1, 100);

                        if (editToLand == 1 || editToLand <= 20)
                        {
                            gridPlane[i, j] = landSpace;
                        }
                    }
                }
            }
        }

        //A method to start the game.
        public override void startGame()
        {
            while (endingBool == false)
            {
                if (endingBool == true)
                {
                    break;
                }
                else
                {
                    playerControl();
                }
            }
        }

        //A method to accumulate the score through gameplay.
        public override void scoreGain(int scoreInput)
        {
            score = score + scoreInput;
        }

        //A method to return the score when commanded.
        public override int finalScore()
        {
            return score;
        }
    }

    //Program class
    class Program
    {
        //Main method
        static void Main(string[] args)
        {
            //diffSwitch allows the switch statement to recieve input. gridGame creates the classes for difficulty.
            int diffSwitch = 0;
            gridGame e1, m1, h1;

            //Displays some messages to entice people to input.
            Console.WriteLine("Pick a difficulty:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Hard");
            diffSwitch = Convert.ToInt32(Console.ReadLine());

            //Switch statement to pick the difficulty based on input.
            switch (diffSwitch)
            {
                //Easy
                case 1:
                    e1 = new easyGrid();
                    e1.playerLand();
                    e1.displayGrid();
                    e1.startGame();
                    Console.WriteLine("Score: " + e1.finalScore());
                    break;
                //Medium
                case 2:
                    m1 = new mediumGrid();
                    m1.playerLand();
                    m1.displayGrid();
                    m1.startGame();
                    Console.WriteLine("Score: " + m1.finalScore());
                    break;
                //Hard
                case 3:
                    h1 = new hardGrid();
                    h1.playerLand();
                    h1.displayGrid();
                    h1.startGame();
                    Console.WriteLine("Score: " + h1.finalScore());
                    break;
            }


        }
    }
}
