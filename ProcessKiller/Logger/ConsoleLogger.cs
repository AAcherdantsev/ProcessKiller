namespace ProcessKiller.Logger;
public class ConsoleLogger : ILogger
{
    public void WriteMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}