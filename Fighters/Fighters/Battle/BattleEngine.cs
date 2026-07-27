using Fighters.Models;
using Fighters.Services;

namespace Fighters.Battle;

public static class BattleEngine
{
    private static readonly Random _random = new();

    public static void Fight( List<Character> fighters )
    {
        if ( fighters.Count < 2 )
        {
            Console.WriteLine( "Нужно минимум 2 бойца для битвы." );
            return;
        }

        var alive = fighters.Where( f => f.IsAlive ).ToList();
        int round = 1;

        while ( alive.Count > 1 )
        {
            Console.WriteLine( $"\n-- Раунд {round} --" );

            foreach ( var fighter in alive.OrderByDescending( f => f.Initiative ).ToList() )
            {
                if ( !fighter.IsAlive || alive.Count <= 1 ) continue;

                var target = alive.Where( f => f != fighter ).ElementAt( _random.Next( alive.Count - 1 ) );

                if ( ChanceCalculator.IsHit( fighter.Initiative, target.Initiative ) )
                {
                    int damage = fighter.CalculateDamage( target );
                    target.ApplyDamage( damage );
                    Console.WriteLine( $"{fighter.Name} нанёс {damage} урона {target.Name}" );
                    Console.WriteLine( $"{target.Name} получил {damage} урона, осталось {target.Health} HP" );
                }
                else
                {
                    Console.WriteLine( $"{fighter.Name} промахнулся по {target.Name}" );
                }

                if ( !target.IsAlive )
                {
                    Console.WriteLine( $"{target.Name} пал в бою!" );
                    alive = fighters.Where( f => f.IsAlive ).ToList();
                }
            }

            round++;
        }

        Console.WriteLine( $"\nПобедитель: {alive[ 0 ].Name}" );
    }
}