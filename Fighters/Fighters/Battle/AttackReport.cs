using Fighters.Models;

namespace Fighters.Battle;

public class AttackReport
{
    public string AttackerName { get; }
    public string DefenderName { get; }
    public int Damage { get; }
    public bool IsHit { get; }
    public bool DefenderDied { get; }

    public AttackReport( string attackerName, string defenderName, int damage, bool isHit, bool defenderDied )
    {
        AttackerName = attackerName;
        DefenderName = defenderName;
        Damage = damage;
        IsHit = isHit;
        DefenderDied = defenderDied;
    }
}