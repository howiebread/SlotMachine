namespace SlotMachine;

public static class Logic
{
     // Method to check for wins.
    public static int CheckForCenter(int[,] grid, int sizeOfGrid)
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

    public static int CheckForVerticals(int[,] grid, int sizeOfGrid)
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

    public static int CheckForDiagonals(int[,] grid, int sizeOfGrid)
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

    public static int CheckForHorizontals(int[,] grid, int sizeOfGrid)
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

    public static int CheckWinnings(string userInput, int[,] slotGrid, int sizeOfGrid, int winnings, int credits)
    {
        List<string> UserChoices = new List<string> {Constants.USER_CHOOSES_CENTERLINE, Constants.USER_CHOOSES_DIAGONALS, Constants.USER_CHOOSES_HORIZONTALS, Constants.USER_CHOOSES_VERTICALS};
        if (UserChoices.Contains(userInput))
        {
            
            if (userInput.ToLower().Trim() == Constants.USER_CHOOSES_CENTERLINE)
            {
                winnings = CheckForCenter(slotGrid, sizeOfGrid);
            }

            else if (userInput.ToLower().Trim() == Constants.USER_CHOOSES_HORIZONTALS)
            {
                winnings = CheckForHorizontals(slotGrid, sizeOfGrid);
            }

            else if (userInput.ToLower().Trim() == Constants.USER_CHOOSES_VERTICALS)
            {
                winnings = CheckForVerticals(slotGrid, sizeOfGrid);
            }

            else if (userInput.ToLower().Trim() == Constants.USER_CHOOSES_DIAGONALS)
            {
                winnings = CheckForDiagonals(slotGrid, sizeOfGrid);
            }
        }
        else
        {
            UI.OutputToUser("Invalid input try again.");
        }  
            
        credits += winnings;
        if (winnings == 0)
        {
            UI.OutputToUser("No matches won try again.");
        }
        return credits;
    }
}