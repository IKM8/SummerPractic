using Fighters.Battle;
using Fighters.Models;
using Fighters.Services;

namespace Fighters.Battle;

public class FightManager( IRandomProvider random )
{
    private readonly AttackManager _attackManager = new( random );

    public FightReport Fight( IReadOnlyList<IFighter> fighters )
    {
        List<RoundReport> rounds = new List<RoundReport>();
        List<IFighter> alive = fighters.Where( f => f.IsAlive ).ToList();

        int roundNumber = 1;

        while ( alive.Count > 1 )
        {
            List<AttackReport> attacks = new List<AttackReport>();

            foreach ( IFighter fighter in alive.OrderByDescending( f => f.Initiative ).ToList() )
            {
                if ( !fighter.IsAlive || alive.Count <= 1 )
                {
                    continue;
                }

                List<IFighter> targets = alive.Where( f => f != fighter ).ToList();
                IFighter target = targets[ random.Next( 0, targets.Count ) ];

                AttackReport report = _attackManager.Attack( fighter, target );
                attacks.Add( report );

                if ( !target.IsAlive )
                {
                    alive = fighters.Where( f => f.IsAlive ).ToList();
                }
            }

            rounds.Add( new RoundReport( roundNumber, attacks ) );
            roundNumber++;
        }

        IFighter? winner = alive.Count == 1 ? alive[ 0 ] : null;

        return new FightReport( rounds, winner );
    }
}