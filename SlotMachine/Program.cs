namespace SlotMachine;

class Program
{
    private const int PRESENT_ROW_NUMBER_FOR_MIDDLE_BORDER = 2;
    private const int BET_AMOUNT = 10;
    private const int SIZE_OF_GRID = 3;
    private const int MINIMUM_RANDOM_NUMBER = 1;
    private const int MAXIMUM_RANDOM_NUMBER = 5;
    private const int DIAGONALWIN_AMOUNT = 15;
    private const string USER_CHOOSES_TO_SPIN = "s";
    private const string USER_CHOOSES_TO_QUIT = "q";
    private static string userInput;
    static int credits = 100;
    static void Main(string[] args)
    {
        // Setting up grid and other game parameters.
        int[,] slotGrid = new int[3, 3];
        Random random = new Random();
        Console.WriteLine("Welcome to Slot Machine!");
        Console.WriteLine("Match three numbers in any row or diagonal to win!");

        while (credits >= BET_AMOUNT)
        {
            Console.WriteLine($"\nCredits: {credits}");
            Console.Write($"Press {USER_CHOOSES_TO_SPIN} to spin or {USER_CHOOSES_TO_QUIT} to quit: ");
            userInput = Console.ReadLine();
            if (userInput == USER_CHOOSES_TO_SPIN)
            {
                credits -= BET_AMOUNT;
                // Fill the grid with random values 1-4
                for (int row = 0; row < SIZE_OF_GRID; row++)
                {
                    for (int col = 0; col < SIZE_OF_GRID; col++)
                    {
                        slotGrid[row, col] = random.Next(MINIMUM_RANDOM_NUMBER, MAXIMUM_RANDOM_NUMBER);
                    }
                }
                // Display the grid.
                Console.WriteLine("\nResults: ");
                // Top border
                Console.WriteLine("┌───┬───┬───┐");
                for (int row = 0; row < SIZE_OF_GRID; row++)
                {
                    // Making a vertical border for row content.
                    Console.Write("|");
                    for (int col = 0; col < SIZE_OF_GRID; col++)
                    {
                        Console.Write($" {slotGrid[row, col]} |");
                    }
                    Console.WriteLine();
                    
                    // Middle or bottom border.
                    if (row < PRESENT_ROW_NUMBER_FOR_MIDDLE_BORDER)
                    {
                        Console.WriteLine("├───┼───┼───┤");
                    }
                    else
                    {
                        Console.WriteLine("└───┴───┴───┘");
                    }
                }
                
                // Check for wins
                int winnings = CheckForWins(slotGrid);
                credits += winnings;

                if (winnings == 0)
                {
                    Console.WriteLine("No matches won, Try again!");
                }
            }
            else if (userInput.ToLower().Trim() == USER_CHOOSES_TO_QUIT)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Try again.");
            }
        }
        Console.WriteLine($"\nGame over! Final credits: {credits}");
        Console.WriteLine("Thank you for playing!");
    }
    // Method to check for wins.
    static int CheckForWins(int[,] grid)
    {
        int totalWins = 0;
        
        // Check horizontal rows for wins
        for (int row = 0; row < SIZE_OF_GRID; row++)
        {
            if (grid[row, 0] == grid[row, 1] && grid[row, 1] == grid[row, 2])
            {
                int value = grid[row, 0];
                int winAmount = value * BET_AMOUNT;
                totalWins += winAmount;
                Console.WriteLine($"There is a match in row {row + 1}! You win {winAmount} credits!");
            }
                
        }
        // Check vertical columns for wins
        for (int col = 0; col < SIZE_OF_GRID; col++)
        {
            if (grid[0, col] == grid[1, col] && grid[1, col] == grid[2, col])
            {
                int value = grid[0, col];
                int winAmount = value * BET_AMOUNT;
                totalWins += winAmount;
                Console.WriteLine($"There is a match in column {col + 1}! You win {winAmount} credits!");
            }
        }
        // Check diagonal from top-left to bottom-right for wins.
        if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2])
        {
            int value = grid[0, 0];
            // Diagonal win pays more credits.
            int winAmount = value * DIAGONALWIN_AMOUNT;
            totalWins += winAmount;
            Console.WriteLine($"There is a match in diagonal (bottom-left to top-right)!  You win {winAmount} credits!");
        }
        // Check diagonal from bottom-left to top-right for wins.
        if (grid[2,0] == grid[1,1] && grid[1,1] == grid[0,2])
        {
            int value = grid[2,0];
            // Diagonal wins pay more credits.
            int winAmount = value * DIAGONALWIN_AMOUNT;
            Console.WriteLine($"There is a match in diagonal (bottom-left to top-right)!  You win {winAmount} credits!");
        }
        return totalWins;
    }
}
