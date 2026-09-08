namespace Fighters.Battle;

public class RoundReport
{
    public int RoundNumber { get; }
    public IReadOnlyList<AttackReport> Attacks { get; }

    public RoundReport( int roundNumber, IReadOnlyList<AttackReport> attacks )
    {
        RoundNumber = roundNumber;
        Attacks = attacks;
    }
}