using Fighters.Services;

namespace Fighters.Tests;

public class ChanceCalculatorTests
{
    [Fact]
    public void CalculateHitChance_EqualInitiative_ReturnsFiftyPercent()
    {
        double chance = ChanceCalculator.CalculateHitChance( 10, 10 );

        Assert.Equal( 0.5, chance );
    }

    [Fact]
    public void CalculateHitChance_HigherInitiative_IncreasesChance()
    {
        double chance = ChanceCalculator.CalculateHitChance( 20, 10 );

        Assert.Equal( 0.9, chance );
    }

    [Fact]
    public void CalculateHitChance_LowerInitiative_DecreasesChance()
    {
        double chance = ChanceCalculator.CalculateHitChance( 10, 20 );

        Assert.Equal( 0.1, chance );
    }

    [Fact]
    public void CalculateHitChance_ExtremeDifference_ClampsToUpperBound()
    {
        double chance = ChanceCalculator.CalculateHitChance( 100, 1 );

        Assert.Equal( 0.9, chance );
    }

    [Fact]
    public void CalculateHitChance_ExtremeNegativeDifference_ClampsToLowerBound()
    {
        double chance = ChanceCalculator.CalculateHitChance( 1, 100 );

        Assert.Equal( 0.1, chance );
    }
}