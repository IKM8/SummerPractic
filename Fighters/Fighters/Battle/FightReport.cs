using Fighters.Models;

namespace Fighters.Battle;

public class FightReport
{
    public IReadOnlyList<RoundReport> Rounds { get; }
    public IFighter? Winner { get; }
    public int TotalRounds => Rounds.Count;

    public FightReport( IReadOnlyList<RoundReport> rounds, IFighter? winner )
    {
        Rounds = rounds;
        Winner = winner;
    }
}