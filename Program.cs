namespace Convays_Game_of_Life
{
    using System.Reflection.Metadata.Ecma335;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class Program
    {
        static void Main(string[] args)
        {
            bool gameOn = true;
            int gameWindowWidth = 15;
            int gameWindowHeight = 15;
            int refreshRate = 125;

            //gamewindow is updated and displayed, while gameReferenceWindow is used to check for neighbours and correctly update gameWindow,
            //so that the state of the array is not dependent on the previous operation but is done all at the same time
            char[,] gameWindow = new char[gameWindowHeight,gameWindowWidth];
            char[,] gameReferenceWindow = new char[gameWindowHeight, gameWindowWidth];

            InstantiateGame(gameWindow);
            InstantiateGame(gameReferenceWindow);

            // Detect game state
            while (gameOn == true) 
            {
                DisplayGame(gameWindow, refreshRate);
                ConvaysGame(gameWindow, gameReferenceWindow);

                //End the loop on command
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    gameOn = false;
                }
            }
        }

        static void InstantiateGame(char[,] gameWindow)
        {
            //Create new and different game window everytime
            Random rand = new Random();

            for (int x = 0; x < gameWindow.GetLength(0); x++)
            {
                for (int y = 0; y < gameWindow.GetLength(1); y++)
                {
                    int randomInt = rand.Next(2);

                    if (randomInt == 0)
                    {
                        gameWindow[x, y] = ' ';
                    }

                    else
                    {
                        gameWindow[x, y] = 'a';
                    }
                }
            }
        }

        static void ConvaysGame(char[,] gameWindow, char[,] gameReferenceWindow)
        {
            /*
             * Logic for Convay's Game of life:
             * Any live cell with fewer than two live neighbors dies, as if by underpopulation.
             * Any live cell with two or three live neighbors lives on to the next generation.
             * Any live cell with more than three live neighbors dies, as if by overpopulation.
             * Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
             */

            // Applying Convay's rules to the array
            for (int x = 0; x < gameReferenceWindow.GetLength(0); x++)
            {
                for (int y = 0; y < gameReferenceWindow.GetLength(1); y++)
                {
                    int neighbours = CheckForNeighbours(gameReferenceWindow, x, y);

                    if (gameReferenceWindow[x, y] == 'a')
                    {
                        if (neighbours < 2)
                        {
                            gameWindow[x, y] = ' ';
                        }

                        else if (neighbours > 3)
                        {
                            gameWindow[x, y] = ' ';
                        }
                    }
                    else
                    {
                        if (neighbours == 3)
                        {
                            gameWindow[x, y] = 'a';
                        }
                    }
                }
            }

            //Update the reference state to the current frame
            for (int x = 0; x < gameWindow.GetLength(0); x++)
            {
                for (int y = 0; y < gameWindow.GetLength(1); y++)
                {
                    gameReferenceWindow[x, y] = gameWindow[x, y];
                }
            }
        }

        static int CheckForNeighbours(char[,] gamewindow, int posX, int posY)
        {
            // Check for neighboring live cells (1s) so that Convay's rules can be applied
            int liveNeighbours = 0;

            try
            {
                if (gamewindow[posX - 1, posY - 1] == 'a')
                {
                    liveNeighbours++;
                }

                if (gamewindow[posX, posY - 1] == 'a')
                {
                    liveNeighbours++;
                }

                if (gamewindow[posX + 1, posY - 1] == 'a')
                {
                    liveNeighbours++;
                }

                if (gamewindow[posX - 1, posY] == 'a')
                {
                    liveNeighbours++;
                }

                if (gamewindow[posX + 1, posY] == 'a')
                {
                    liveNeighbours++;
                }

                if (gamewindow[posX - 1, posY + 1] == 'a')
                {
                    liveNeighbours++;
                }

                if (gamewindow[posX, posY + 1] == 'a')
                {
                    liveNeighbours++;
                }

                if (gamewindow[posX + 1, posY + 1] == 'a')
                {
                    liveNeighbours++;
                }
            }

            catch (IndexOutOfRangeException e)
            {
                // Cool man.
            }

            return liveNeighbours;
        }

        static int NumberofLiveCells(char[,] gameWindow)
        {
            int liveCells = 0;
            foreach (char a in gameWindow)
            {
                if (a == 'a')
                {
                    liveCells++;
                }
            }

            return liveCells;
        }

        static void DisplayGame(char[,] gameWindow, int refreshRate)
        {
            //Displaying the game window with the given state of the array
            for (int x = 0; x < gameWindow.GetLength(0); x++)
            {
                for (int y = 0; y < gameWindow.GetLength(1); y++)
                {
                    if (gameWindow[x, y] == 'a')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(gameWindow[x, y].ToString() + " ");
                    }

                    else
                    {
                        Console.Write(gameWindow[x, y].ToString() + " ");
                    }
                }

                Console.WriteLine("");
            }

            Thread.Sleep(refreshRate);
            Console.Clear();
        }
    }
}
