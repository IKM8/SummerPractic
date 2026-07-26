namespace Fighters.Models;

public interface IFighter
{
    string Name { get; }
    bool IsAlive { get; }
    void Attack( IFighter target );
}