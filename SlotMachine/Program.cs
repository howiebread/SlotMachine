﻿namespace SlotMachine;

class Program
{
    private const int STARTING_INDEX = 0;
    private const int RESET_VALUE = 0;
    private const int STARTING_CREDITS = 100;
    private const int ADD_TO_CURRENT_INDEX = 1;
    private const int ROW_NUMBER_NEEDED_FOR_MIDDLE_BORDER = 2;
    private const int BET_AMOUNT = 10;
    private const int SIZE_OF_GRID = 3;
    private const int MINIMUM_RANDOM_NUMBER = 1;
    private const int MAXIMUM_RANDOM_NUMBER = 5;
    private const int DIAGONAL_WIN_AMOUNT = 15;
    private const string USER_CHOOSES_CENTERLINE = "c";
    private const string USER_CHOOSES_HORIZONTALS = "h";
    private const string USER_CHOOSES_VERTICALS = "v";
    private const string USER_CHOOSES_DIAGONALS = "d";
    private const string USER_CHOOSES_TO_QUIT = "q";
    private static string userInput;
    static int credits = STARTING_CREDITS;
    private static int winnings;

    static void Main(string[] args)
    {
        // Setting up grid and other game parameters.
        int[,] slotGrid = new int[3, 3];
        Random random = new Random();
        Console.WriteLine("Welcome to Slot Machine!");
        Console.WriteLine("Match three numbers in the mode you select to win!");

        while (credits >= BET_AMOUNT)
        {
            Console.WriteLine($"\nCredits: {credits}");
            Console.Write($"Choose which mode to play.  Press {USER_CHOOSES_CENTERLINE} for center line, {USER_CHOOSES_HORIZONTALS} for horizontals, {USER_CHOOSES_VERTICALS} for verticals, {USER_CHOOSES_DIAGONALS} for diagonals, or {USER_CHOOSES_TO_QUIT} to quit: ");
            userInput = Console.ReadLine();
            if (userInput == USER_CHOOSES_TO_QUIT)
            {
                break;
            }

            if (userInput == USER_CHOOSES_CENTERLINE || userInput == USER_CHOOSES_HORIZONTALS || userInput == USER_CHOOSES_VERTICALS || userInput == USER_CHOOSES_DIAGONALS)
            {
                credits -= BET_AMOUNT;
                winnings = RESET_VALUE;  // Resets winnings at the start of each round.
                // Fill the grid with random values 1-4
                for (int row = STARTING_INDEX; row < SIZE_OF_GRID; row++)
                {
                    for (int col = STARTING_INDEX; col < SIZE_OF_GRID; col++)
                    {
                        slotGrid[row, col] = random.Next(MINIMUM_RANDOM_NUMBER, MAXIMUM_RANDOM_NUMBER);
                    }
                }

                // Display the grid.
                Console.WriteLine("\nResults: ");
                // Top border
                Console.WriteLine("┌───┬───┬───┐");
                for (int row = STARTING_INDEX; row < SIZE_OF_GRID; row++)
                {
                    // Making a vertical border for row content.
                    Console.Write("|");
                    for (int col = STARTING_INDEX; col < SIZE_OF_GRID; col++)
                    {
                        Console.Write($" {slotGrid[row, col]} |");
                    }

                    Console.WriteLine();

                    // Middle or bottom border.
                    if (row < ROW_NUMBER_NEEDED_FOR_MIDDLE_BORDER)
                    {
                        Console.WriteLine("├───┼───┼───┤");
                    }
                    else
                    {
                        Console.WriteLine("└───┴───┴───┘");
                    }
                }
                 
            }
              
            if (userInput.ToLower().Trim() == USER_CHOOSES_CENTERLINE)
            {
                winnings = CheckForCenter(slotGrid);
            }

            else if (userInput.ToLower().Trim() == USER_CHOOSES_HORIZONTALS)
            {
               winnings = CheckForHorizontals(slotGrid);
            }

            else if (userInput.ToLower().Trim() == USER_CHOOSES_VERTICALS)
            {
                winnings = CheckForVerticals(slotGrid);
            }

            else if (userInput.ToLower().Trim() == USER_CHOOSES_DIAGONALS)
            {
                winnings = CheckForDiagonals(slotGrid);
            }
            
            else
            {
                Console.WriteLine("Invalid input. Try again.");
            }  
            
            credits += winnings;
            if (winnings == 0)
            {
                Console.WriteLine("No matches won try again.");
            }
        }
       
        Console.WriteLine($"\nGame over! Final credits: {credits}");
        Console.WriteLine("Thank you for playing!");
    }

        
      
    
        
    // Method to check for wins.
    static int CheckForCenter(int[,] grid)
    {
        int totalWins = 0;
        int row = 1;
        // Check center row for wins.
        if (grid[row, 0] == grid[row, 1] && grid[row, 1] == grid[row, 2])
        {
            int value = grid[row, 0];
            int winAmount = value + BET_AMOUNT;
            totalWins += winAmount;
            Console.WriteLine($"There is a match in row {row + ADD_TO_CURRENT_INDEX}! You win {winAmount} credits!");
        }

        return totalWins;
    }

    static int CheckForVerticals(int[,] grid)
    {
        int totalWins = 0;
        // Check vertical rows for wins.
        for (int col = STARTING_INDEX; col < SIZE_OF_GRID; col++)
        {
            if (grid[0, col] == grid[1, col] && grid[1, col] == grid[2, col])
            {
                int value = grid[0, col];
                int winAmount = value + BET_AMOUNT;
                totalWins += winAmount;
                Console.WriteLine($"There is a match in column {col + ADD_TO_CURRENT_INDEX}! You win {winAmount} credits!");
            }
        }

        return totalWins;
    }

    static int CheckForDiagonals(int[,] grid)
    {
        int totalWins = 0;

        // Check diagonal from top-left to bottom-right for wins.
        if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2])
        {
            int value = grid[0, 0];
            // Diagonal win pays more credits.
            int winAmount = value + DIAGONAL_WIN_AMOUNT;
            totalWins += winAmount;
            Console.WriteLine($"There is a match in diagonal (top-left to bottom-right)!  You win {winAmount} credits!");
        }

        // Check diagonal from bottom-left to top-right for wins.
        if (grid[2, 0] == grid[1, 1] && grid[1, 1] == grid[0, 2])
        {
            int value = grid[2, 0];
            // Diagonal wins pay more credits.
            int winAmount = value + DIAGONAL_WIN_AMOUNT;
            Console.WriteLine($"There is a match in diagonal (bottom-left to top-right)!  You win {winAmount} credits!");
            totalWins += winAmount;
        }

        return totalWins;
    }

    static int CheckForHorizontals(int[,] grid)
    {
        int totalWins = 0;

        // Check horizontal rows for wins
        for (int row = 0; row < SIZE_OF_GRID; row++)
        {
            if (grid[row, 0] == grid[row, 1] && grid[row, 1] == grid[row, 2])
            {
                int value = grid[row, 0];
                int winAmount = value + BET_AMOUNT;
                totalWins += winAmount;
                Console.WriteLine($"There is a match in row {row + ADD_TO_CURRENT_INDEX}! You win {winAmount} credits!");
            }
        }
        return totalWins;
    }
}