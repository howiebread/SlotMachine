namespace SlotMachine;

public static class UI
{
    
    public static void OutputToUser(string message)
    {
        Console.WriteLine(message);
    }
    
    public static string TakeInput()
    {
        return Console.ReadLine();
    }

    public static void AddLine()
    {
        Console.WriteLine();
    }

    public static void OutputToUserSameLine(string message)
    {
        Console.Write(message);
    }

    public static void DisplayGrid(int sizeOfGrid, int [,]slotGrid)
    {
        for (int row = 0; row < sizeOfGrid; row++)
        {
            for (int col = 0; col < sizeOfGrid; col++)
            {
                Console.Write($"{slotGrid[row, col]} ");
            }
            Console.WriteLine(); // New line after each row
        }
    }

    public static void FillGrid(int sizeOfGrid, int [,]slotGrid)
    {
        Random random = new Random();
        for (int row = Constants.STARTING_INDEX; row < sizeOfGrid; row++)
        {
            for (int col = Constants.STARTING_INDEX; col < sizeOfGrid; col++)
            {
                slotGrid[row, col] = random.Next(Constants.MINIMUM_RANDOM_NUMBER, Constants.MAXIMUM_RANDOM_NUMBER);
            }
        }
    }
}
