namespace Fighters.ConsoleUi;

public interface IConsole
{
    void WriteLine( string line );
    string? ReadLine();
}