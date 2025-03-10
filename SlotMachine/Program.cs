namespace SlotMachine;

class Program
{
    private const int STARTING_INDEX = 0;
    private const int RESET_VALUE = 0;
    private const int STARTING_CREDITS = 100;
    private const int ADD_TO_CURRENT_INDEX = 1;
    private const int ROW_NUMBER_NEEDED_FOR_MIDDLE_BORDER = 2;
    private const int BET_AMOUNT = 10;
    private const int DEFAULT_SIZE_OF_GRID = 3;
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
    private static int sizeOfGrid = DEFAULT_SIZE_OF_GRID;

    static void Main(string[] args)
    {
        // Ask user for the grid size.
        
        Console.WriteLine("Welcome to Slot Machine!");
        Console.WriteLine("Match three numbers in the mode you select to win!");
        Console.WriteLine("Enter grid size (3 for 3x3, 4 for 4x4, etc.):");
        try
        {
            sizeOfGrid = Convert.ToInt32(Console.ReadLine());
            if (sizeOfGrid < 3)
            {
                sizeOfGrid = 3; // Setting default grid size if user selects number smaller than 3.
            }
        }
        catch
        {
            Console.WriteLine("Invalid input.  Using default grid size of 3.");
            sizeOfGrid = 3;
        }
        // Setting up grid and other game parameters.
        int[,] slotGrid = new int[sizeOfGrid, sizeOfGrid];
        Random random = new Random();

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
                for (int row = STARTING_INDEX; row < sizeOfGrid; row++)
                {
                    for (int col = STARTING_INDEX; col < sizeOfGrid; col++)
                    {
                        slotGrid[row, col] = random.Next(MINIMUM_RANDOM_NUMBER, MAXIMUM_RANDOM_NUMBER);
                    }
                }

                // Display the grid.
                Console.WriteLine("\nResults: ");
                
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
       
    }

    static int CheckForVerticals(int[,] grid)
    {
        int totalWins = 0;
        // Check vertical rows for wins.
        for (int col = STARTING_INDEX; col < sizeOfGrid; col++)
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
        for (int row = 0; row < sizeOfGrid; row++)
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