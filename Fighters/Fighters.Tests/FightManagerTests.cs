using Fighters.Battle;
using Fighters.Models;
using Fighters.Services;
using Moq;

namespace Fighters.Tests;

public class FightManagerTests
{
    private static Mock<IRandomProvider> CreateRandomMock( params double[] rolls )
    {
        Mock<IRandomProvider> random = new Mock<IRandomProvider>();
        Queue<double> queue = new Queue<double>( rolls );
        random.Setup( r => r.NextDouble() ).Returns( () => queue.Count > 0 ? queue.Dequeue() : 0.1 );
        random.Setup( r => r.Next( It.IsAny<int>(), It.IsAny<int>() ) ).Returns( 0 );
        return random;
    }

    [Fact]
    public void Fight_TwoFighters_ReturnsWinner()
    {
        Mock<IRandomProvider> random = CreateRandomMock();
        FightManager manager = new FightManager( random.Object );

        Character first = new Character( "Боец1", Race.Orc, Weapon.Axe, Armor.None, CharacterClass.Barbarian );
        Character second = new Character( "Боец2", Race.Human, Weapon.Fists, Armor.None, CharacterClass.Mercenary );

        FightReport report = manager.Fight( new List<IFighter> { first, second } );

        Assert.NotNull( report.Winner );
        Assert.True( report.Winner.IsAlive );
        Assert.True( report.TotalRounds > 0 );
    }

    [Fact]
    public void Fight_LessThanTwoFighters_ReturnsNoWinner()
    {
        Mock<IRandomProvider> random = CreateRandomMock();
        FightManager manager = new FightManager( random.Object );

        Character single = new Character( "Боец", Race.Orc, Weapon.Axe, Armor.None, CharacterClass.Barbarian );

        FightReport report = manager.Fight( new List<IFighter> { single } );

        Assert.Null( report.Winner );
        Assert.Empty( report.Rounds );
    }

    [Fact]
    public void Fight_FighterWithHigherInitiative_AttacksFirstInFirstRound()
    {
        Mock<IRandomProvider> random = CreateRandomMock( 0.1 );
        FightManager manager = new FightManager( random.Object );

        Character fast = new Character( "Быстрый", Race.Elf, Weapon.Bow, Armor.None, CharacterClass.Assassin );
        Character slow = new Character( "Медленный", Race.Orc, Weapon.Fists, Armor.None, CharacterClass.Knight );

        FightReport report = manager.Fight( new List<IFighter> { slow, fast } );

        Assert.NotNull( report.Rounds.FirstOrDefault() );
        Assert.Equal( "Быстрый", report.Rounds[ 0 ].Attacks[ 0 ].AttackerName );
    }

    [Fact]
    public void Fight_WinnerIsTheOnlyAliveFighter()
    {
        Mock<IRandomProvider> random = CreateRandomMock();
        FightManager manager = new FightManager( random.Object );

        Character strong = new Character( "Сильный", Race.Orc, Weapon.Axe, Armor.Plate, CharacterClass.Barbarian );
        Character weak = new Character( "Слабый", Race.Goblin, Weapon.Fists, Armor.None, CharacterClass.Mercenary );

        FightReport report = manager.Fight( new List<IFighter> { strong, weak } );

        Assert.Equal( "Сильный", report.Winner?.Name );
        Assert.Equal( strong.MaxHealth, strong.Health );
        Assert.False( weak.IsAlive );
    }
}