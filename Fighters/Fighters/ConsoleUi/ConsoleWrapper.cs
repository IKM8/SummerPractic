namespace Fighters.ConsoleUi;

public class ConsoleWrapper : IConsole
{
    public void WriteLine( string line )
    {
        Console.WriteLine( line );
    }

    public string? ReadLine()
    {
        return Console.ReadLine();
    }
}