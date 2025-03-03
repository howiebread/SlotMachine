namespace SlotMachine;

class Program
{
    
    const int BET_AMOUNT = 10;
    private const int DIAGONALWIN_AMOUNT = 15;
    private const string USER_CHOOSES_TO_SPIN = "s";
    private const string USER_CHOOSES_TO_QUIT = "q";
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
            Console.WriteLine($"/nCredits: {credits}");
            Console.Write($"Press {USER_CHOOSES_TO_SPIN} to spin or {USER_CHOOSES_TO_QUIT} to quit: ");
            string userInput = Console.ReadLine();
            if (userInput == USER_CHOOSES_TO_SPIN)
            {
                credits -= BET_AMOUNT;
                // Fill the grid with random values 1-6
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        slotGrid[row, col] = random.Next(1, 7);
                    }
                }
                // Display the grid.
                Console.WriteLine("/nResults: ");
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        Console.Write($"{slotGrid[row, col]} ");
                    }
                    Console.WriteLine();
                }
            }
            if (userInput.ToLower().Trim() == USER_CHOOSES_TO_QUIT)
            {
                break;
            }
        }
    }

    static int checkForWins(int[,] grid)
    {
        int totalWins = 0;
        
        // Check horizontal rows for wins
        for (int row = 0; row < 3; row++)
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
        for (int col = 0; col < 3; col++)
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
