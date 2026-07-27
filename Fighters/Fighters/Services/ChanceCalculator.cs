namespace Fighters.Services;

public static class ChanceCalculator
{
    private static readonly Random _random = new();

    public static double CalculateHitChance( int attackerInitiative, int defenderInitiative )
    {
        int diff = attackerInitiative - defenderInitiative;
        return Math.Clamp( 0.5 + diff * 0.05, 0.1, 0.9 );
    }

    public static bool IsHit( int attackerInitiative, int defenderInitiative )
    {
        double chance = CalculateHitChance( attackerInitiative, defenderInitiative );
        return _random.NextDouble() < chance;
    }
}