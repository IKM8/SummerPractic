using Fighters.Models;

namespace Fighters.Tests;

public class CharacterTests
{
    [Fact]
    public void Constructor_OrcWithAxeAndPlateBarbarian_CalculatesStatsCorrectly()
    {
        Character character = new Character( "Тест", Race.Orc, Weapon.Axe, Armor.Plate, CharacterClass.Barbarian );

        Assert.Equal( 135, character.MaxHealth );
        Assert.Equal( 30, character.Strength );
        Assert.Equal( 12, character.Defense );
        Assert.Equal( 8, character.Initiative );
        Assert.True( character.IsAlive );
    }

    [Fact]
    public void Constructor_ElfWithBowAndLeatherAssassin_CalculatesStatsCorrectly()
    {
        Character character = new Character( "Тест", Race.Elf, Weapon.Bow, Armor.Leather, CharacterClass.Assassin );

        Assert.Equal( 88, character.MaxHealth );
        Assert.Equal( 21, character.Strength );
        Assert.Equal( 6, character.Defense );
        Assert.Equal( 10, character.Initiative );
    }

    [Fact]
    public void ApplyDamage_WhenDamageLessThanHealth_ReducesHealth()
    {
        Character character = new Character( "Тест", Race.Human, Weapon.Fists, Armor.None, CharacterClass.Mercenary );

        character.ApplyDamage( 30 );

        Assert.Equal( character.MaxHealth - 30, character.Health );
    }

    [Fact]
    public void ApplyDamage_WhenDamageExceedsHealth_SetsHealthToZero()
    {
        Character character = new Character( "Тест", Race.Human, Weapon.Fists, Armor.None, CharacterClass.Mercenary );

        character.ApplyDamage( 10000 );

        Assert.Equal( 0, character.Health );
        Assert.False( character.IsAlive );
    }

    [Fact]
    public void HealFull_AfterDamage_RestoresMaxHealth()
    {
        Character character = new Character( "Тест", Race.Human, Weapon.Fists, Armor.None, CharacterClass.Mercenary );
        character.ApplyDamage( 50 );

        character.HealFull();

        Assert.Equal( character.MaxHealth, character.Health );
    }
}