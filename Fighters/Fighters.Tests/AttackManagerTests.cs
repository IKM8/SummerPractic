using Fighters.Battle;
using Fighters.Models;
using Fighters.Services;
using Moq;

namespace Fighters.Tests;

public class AttackManagerTests
{
    [Fact]
    public void Attack_WhenRandomRollBelowChance_DealsDamage()
    {
        Mock<IRandomProvider> random = new Mock<IRandomProvider>();
        random.Setup( r => r.NextDouble() ).Returns( 0.1 );

        AttackManager manager = new AttackManager( random.Object );
        Character attacker = new Character( "Атакующий", Race.Orc, Weapon.Axe, Armor.None, CharacterClass.Barbarian );
        Character defender = new Character( "Защитник", Race.Human, Weapon.Fists, Armor.ChainMail, CharacterClass.Knight );

        AttackReport report = manager.Attack( attacker, defender );

        Assert.True( report.IsHit );
        Assert.True( report.Damage > 0 );
        Assert.Equal( attacker.Strength - defender.Defense, report.Damage );
    }

    [Fact]
    public void Attack_WhenRandomRollAboveChance_Misses()
    {
        Mock<IRandomProvider> random = new Mock<IRandomProvider>();
        random.Setup( r => r.NextDouble() ).Returns( 0.99 );

        AttackManager manager = new AttackManager( random.Object );
        Character attacker = new Character( "Атакующий", Race.Orc, Weapon.Axe, Armor.None, CharacterClass.Barbarian );
        Character defender = new Character( "Защитник", Race.Human, Weapon.Fists, Armor.ChainMail, CharacterClass.Knight );

        AttackReport report = manager.Attack( attacker, defender );

        Assert.False( report.IsHit );
        Assert.Equal( 0, report.Damage );
        Assert.Equal( defender.MaxHealth, defender.Health );
    }

    [Fact]
    public void Attack_WhenDefenseHigherThanStrength_DealsZeroDamage()
    {
        Mock<IRandomProvider> random = new Mock<IRandomProvider>();
        random.Setup( r => r.NextDouble() ).Returns( 0.1 );

        AttackManager manager = new AttackManager( random.Object );
        Character attacker = new Character( "Слабак", Race.Goblin, Weapon.Fists, Armor.None, CharacterClass.Mercenary );
        Character defender = new Character( "Танк", Race.Orc, Weapon.Fists, Armor.Plate, CharacterClass.Knight );

        AttackReport report = manager.Attack( attacker, defender );

        Assert.True( report.IsHit );
        Assert.Equal( 0, report.Damage );
        Assert.True( defender.IsAlive );
    }

    [Fact]
    public void Attack_WhenDamageKillsDefender_SetsDefenderDied()
    {
        Mock<IRandomProvider> random = new Mock<IRandomProvider>();
        random.Setup( r => r.NextDouble() ).Returns( 0.1 );

        AttackManager manager = new AttackManager( random.Object );
        Character attacker = new Character( "Силач", Race.Orc, Weapon.Axe, Armor.None, CharacterClass.Barbarian );
        Character defender = new Character( "Слабая", Race.Goblin, Weapon.Fists, Armor.None, CharacterClass.Assassin );

        defender.ApplyDamage( defender.MaxHealth - 1 );

        AttackReport report = manager.Attack( attacker, defender );

        Assert.True( report.IsHit );
        Assert.True( report.DefenderDied );
        Assert.False( defender.IsAlive );
    }
}