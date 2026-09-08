using Fighters.Battle;
using Fighters.Models;
using Fighters.Services;

namespace Fighters.Services;

public class AttackManager( IRandomProvider random )
{
    public AttackReport Attack( IFighter attacker, IFighter defender )
    {
        double chance = ChanceCalculator.CalculateHitChance( attacker.Initiative, defender.Initiative );
        bool isHit = random.NextDouble() < chance;

        if ( !isHit )
        {
            return new AttackReport( attacker.Name, defender.Name, 0, false, false );
        }

        int damage = Math.Max( attacker.Strength - defender.Defense, 0 );
        defender.ApplyDamage( damage );

        return new AttackReport( attacker.Name, defender.Name, damage, true, !defender.IsAlive );
    }
}