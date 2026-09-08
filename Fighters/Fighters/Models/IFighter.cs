namespace Fighters.Models;

public interface IFighter
{
    string Name { get; }
    int Health { get; }
    int MaxHealth { get; }
    int Strength { get; }
    int Defense { get; }
    int Initiative { get; }
    bool IsAlive { get; }
    void ApplyDamage( int damage );
}