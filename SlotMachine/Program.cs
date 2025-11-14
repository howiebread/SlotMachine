namespace SlotMachine;

class Program
{
    
    private static string userInput;
    static int credits = Constants.STARTING_CREDITS;
    private static int winnings;
    private static int sizeOfGrid = Constants.DEFAULT_SIZE_OF_GRID;
    private static int index;

    static void Main(string[] args)
    {
        // Ask user for the grid size.
        
        UI.OutputToUser("Welcome to Slot Machine!");
        UI.OutputToUser("Match three numbers in the mode you select to win!");
        UI.OutputToUser("Enter an odd number for grid size (3 for 3x3, 5 for 5x5 etc.):");
        try
        {
            sizeOfGrid = Convert.ToInt32(Console.ReadLine());
            // Check if the number is even.
            if (sizeOfGrid % 2 == 0)
            {
               UI.OutputToUser($"The grid size must be an odd number. Using default grid size of {Constants.DEFAULT_SIZE_OF_GRID}.");
               sizeOfGrid = Constants.DEFAULT_SIZE_OF_GRID;
            }

            if (sizeOfGrid < Constants.DEFAULT_SIZE_OF_GRID)
            {
                sizeOfGrid = Constants.DEFAULT_SIZE_OF_GRID;
                UI.OutputToUser($"Grid size is too small.  Using the minimum size of {Constants.DEFAULT_SIZE_OF_GRID} ");
            }
        }
        catch
        {
            UI.OutputToUser($"Invalid input.  Using default grid size of {Constants.DEFAULT_SIZE_OF_GRID}.");
            sizeOfGrid = Constants.DEFAULT_SIZE_OF_GRID;
        }
        // Setting up grid and other game parameters.
        int[,] slotGrid = new int[sizeOfGrid, sizeOfGrid];
        Random random = new Random();

        while (credits >= Constants.BET_AMOUNT)
        {
            UI.OutputToUser($"\nCredits: {credits}");
            UI.OutputToUser($"Choose which mode to play.  Press {Constants.USER_CHOOSES_CENTERLINE} for center line, {Constants.USER_CHOOSES_HORIZONTALS} for horizontals, {Constants.USER_CHOOSES_VERTICALS} for verticals, {Constants.USER_CHOOSES_DIAGONALS} for diagonals, or {Constants.USER_CHOOSES_TO_QUIT} to quit: ");
            userInput = UI.TakeInput();
            // Create a list of user choices.
            List<string> choices = new List<string> { Constants.USER_CHOOSES_DIAGONALS, Constants.USER_CHOOSES_VERTICALS, Constants.USER_CHOOSES_CENTERLINE, Constants.USER_CHOOSES_HORIZONTALS };
            if (userInput == Constants.USER_CHOOSES_TO_QUIT)
            {
                break;
            }
            
            if (choices.Contains(userInput))
            {
                credits -= Constants.BET_AMOUNT;
                winnings = Constants.RESET_VALUE;  // Resets winnings at the start of each round.
                // Fill the grid with random values 1-4
                for (int row = Constants.STARTING_INDEX; row < sizeOfGrid; row++)
                {
                    for (int col = Constants.STARTING_INDEX; col < sizeOfGrid; col++)
                    {
                        slotGrid[row, col] = random.Next(Constants.MINIMUM_RANDOM_NUMBER, Constants.MAXIMUM_RANDOM_NUMBER);
                    }
                }

                // Display the grid.
                UI.OutputToUser("\nResults: ");
                for (int row = 0; row < sizeOfGrid; row++)
                {
                    for (int col = 0; col < sizeOfGrid; col++)
                    {
                        UI.OutputToUser($"{slotGrid[row, col]} ");
                    }
                    UI.AddLine(); // New line after each row
                }
                
            }
              
            if (userInput.ToLower().Trim() == Constants.USER_CHOOSES_CENTERLINE)
            {
                winnings = CheckForCenter(slotGrid);
            }

            else if (userInput.ToLower().Trim() == Constants.USER_CHOOSES_HORIZONTALS)
            {
               winnings = CheckForHorizontals(slotGrid);
            }

            else if (userInput.ToLower().Trim() == Constants.USER_CHOOSES_VERTICALS)
            {
                winnings = CheckForVerticals(slotGrid);
            }

            else if (userInput.ToLower().Trim() == Constants.USER_CHOOSES_DIAGONALS)
            {
                winnings = CheckForDiagonals(slotGrid);
            }
            
            else
            {
                UI.OutputToUser("Invalid input. Try again.");
            }  
            
            credits += winnings;
            if (winnings == 0)
            {
                UI.OutputToUser("No matches won try again.");
            }
        }
       
        UI.OutputToUser($"\nGame over! Final credits: {credits}");
        UI.OutputToUser("Thank you for playing!");
    }
    
    // Method to check for wins.
    static int CheckForCenter(int[,] grid)
    {
        int totalWins = 0;
        int row = sizeOfGrid / Constants.NUMBER_TO_DIVIDE_FOR_MIDDLE_ROW; // This will get us the middle row.
        // Check center row for wins.
        bool allMatch = true;
        int firstValue = grid[row, 0];

        for (int col = 1; col < sizeOfGrid; col++)
        {
            if (grid[row, col] != firstValue)
            {
                allMatch = false;
                break;
            }
        }

        if (allMatch)
        {
            int winAmount = firstValue + Constants.BET_AMOUNT;
            totalWins += winAmount;
            Console.WriteLine($"There is a match in the center row!  You win {winAmount} credits!");
        }
        return totalWins;
    }

    static int CheckForVerticals(int[,] grid)
    {
        int totalWins = 0;
        // Check vertical rows for wins.
        for (int col = Constants.STARTING_INDEX; col < sizeOfGrid; col++)
        {
            bool allMatch = true;
            int firstValue = grid[0, col];

            for (int row = 1; row < sizeOfGrid; row++)
            {
                if (grid[row, col] != firstValue)
                {
                    allMatch = false;
                    break;
                }
            }

            if (allMatch)
            {
                int winAmount = firstValue + Constants.BET_AMOUNT;
                totalWins += winAmount;
                Console.WriteLine($"There is a match in column {col + Constants.ADD_TO_CURRENT_INDEX}!  You win {winAmount} credits!");
            }
        }
        return totalWins;
    }

    static int CheckForDiagonals(int[,] grid)
    {
        int totalWins = 0;

        // Check diagonal from top-left to bottom-right for wins.
        bool allMatch = true;
        int firstValue = grid[0, 0];

        for (int index = 1; index < sizeOfGrid; index++)
        {
            if (grid[index, index] != firstValue)
            {
                allMatch = false;
                break;
            }
        }

        if (allMatch)
        {
            int winAmount = firstValue + Constants.DIAGONAL_WIN_AMOUNT;
            totalWins += winAmount;
            Console.WriteLine($"There is a match in diagonal (top-left to bottom-right)!  You win {winAmount} credits!");
        }

        // Check diagonal from bottom-left to top-right for wins.
       allMatch = true;
       firstValue = grid[sizeOfGrid - Constants.SUBTRACT_TO_CURRENT_INDEX, 0];

       for (int index = 1; index < sizeOfGrid; index++)
       {
           if (grid[sizeOfGrid - Constants.SUBTRACT_TO_CURRENT_INDEX - index, index] != firstValue)
           {
               allMatch = false;
               break;
           }
       }

       if (allMatch)
       {
           int winAmount = firstValue + Constants.DIAGONAL_WIN_AMOUNT;
           Console.WriteLine($"There is a match in diagonal (bottom-left to top-right)!  You win {winAmount} credits!");
           totalWins += winAmount;
       }
        return totalWins;
    }

    static int CheckForHorizontals(int[,] grid)
    {
        int totalWins = 0;
        bool foundMatch = false;  

        // Check horizontal rows for wins
        for (int row = 0; row < sizeOfGrid; row++)
        {
            bool allMatch = true;
            int firstValue = grid[row, 0];

            for (int col = 1; col < sizeOfGrid; col++)
            {
                if (grid[row, col] != firstValue)
                {
                    allMatch = false;
                    break;
                }
            }

            if (allMatch && !foundMatch)
            {
                int winAmount = firstValue + Constants.BET_AMOUNT;
                totalWins += winAmount;
                foundMatch = true;
                Console.WriteLine($"There is a match in row {row + Constants.ADD_TO_CURRENT_INDEX}!  You win {winAmount} credits!");
            }
            else if (allMatch)
            {
                Console.WriteLine($"There is also a match in row {row + Constants.ADD_TO_CURRENT_INDEX}, but only one win is counted!");
            }
        }
        return totalWins;
    }
}