using Fighters.Battle;
using Fighters.ConsoleUi;
using Fighters.Services;

namespace Fighters;

public static class Program
{
    public static void Main()
    {
        IConsole console = new ConsoleWrapper();
        ConsoleInput input = new ConsoleInput( console );
        IRandomProvider random = new RandomProvider();
        FightManager fightManager = new FightManager( random );
        GameApp app = new GameApp( console, input, fightManager );

        app.Run();
    }
}