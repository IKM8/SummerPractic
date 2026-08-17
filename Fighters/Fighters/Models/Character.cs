namespace Fighters.Models;

public class Character : IFighter
{
    public string Name { get; }
    public Race Race { get; }
    public Weapon Weapon { get; }
    public Armor Armor { get; }
    public CharacterClass Class { get; }
    public bool IsAlive => Health > 0;

    public int Health { get; private set; }
    public int MaxHealth { get; }
    public int Strength { get; }
    public int Defense { get; }
    public int Initiative { get; }

    public Character( string name, Race race, Weapon weapon, Armor armor, CharacterClass characterClass )
    {
        Name = name;
        Race = race;
        Weapon = weapon;
        Armor = armor;
        Class = characterClass;

        int raceHp = race switch
        {
            Race.Human => 100,
            Race.Elf => 80,
            Race.Orc => 120,
            Race.Goblin => 70,
            _ => 100
        };

        int raceStr = race switch
        {
            Race.Human => 5,
            Race.Elf => 7,
            Race.Orc => 10,
            Race.Goblin => 4,
            _ => 5
        };

        int raceDef = race switch
        {
            Race.Human => 5,
            Race.Elf => 3,
            Race.Orc => 2,
            Race.Goblin => 4,
            _ => 5
        };

        int weaponDmg = weapon switch
        {
            Weapon.Fists => 2,
            Weapon.Sword => 8,
            Weapon.Axe => 10,
            Weapon.Bow => 6,
            Weapon.Staff => 4,
            _ => 2
        };

        int armorDef = armor switch
        {
            Armor.None => 0,
            Armor.Leather => 3,
            Armor.ChainMail => 6,
            Armor.Plate => 10,
            _ => 0
        };

        int classHp = characterClass switch
        {
            CharacterClass.Mercenary => 10,
            CharacterClass.Knight => 20,
            CharacterClass.Barbarian => 15,
            CharacterClass.Assassin => 8,
            _ => 10
        };

        int classStr = characterClass switch
        {
            CharacterClass.Mercenary => 5,
            CharacterClass.Knight => 3,
            CharacterClass.Barbarian => 10,
            CharacterClass.Assassin => 8,
            _ => 5
        };

        Initiative = characterClass switch
        {
            CharacterClass.Knight => 5,
            CharacterClass.Barbarian => 8,
            CharacterClass.Mercenary => 7,
            CharacterClass.Assassin => 10,
            _ => 5
        };

        MaxHealth = raceHp + classHp;
        Health = MaxHealth;
        Strength = raceStr + weaponDmg + classStr;
        Defense = raceDef + armorDef;
    }

    public void Attack( IFighter target )
    {
        int damage = Math.Max( Strength - ( ( Character )target ).Defense, 0 );
        ( ( Character )target ).Health -= damage;
        Console.WriteLine( $"{Name} нанёс {damage} урона {target.Name}" );
    }

    public int CalculateDamage( IFighter target )
    {
        return Math.Max( Strength - ( ( Character )target ).Defense, 0 );
    }

    public void ApplyDamage( int damage )
    {
        Health = Math.Max( 0, Health - damage );
    }

    public void HealFull()
    {
        Health = MaxHealth;
    }

    public override string ToString()
    {
        return $"{Name} [{Race} {Class}] | HP: {Health}/{MaxHealth} | Str: {Strength} | Def: {Defense} | {Weapon} + {Armor}";
    }
}