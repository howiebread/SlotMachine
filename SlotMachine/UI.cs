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
}
