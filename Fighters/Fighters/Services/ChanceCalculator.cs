namespace Fighters.Services;

public static class ChanceCalculator
{
    public static double CalculateHitChance( int attackerInitiative, int defenderInitiative )
    {
        int diff = attackerInitiative - defenderInitiative;
        return Math.Clamp( 0.5 + diff * 0.05, 0.1, 0.9 );
    }
}