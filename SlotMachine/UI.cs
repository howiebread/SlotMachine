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
}
