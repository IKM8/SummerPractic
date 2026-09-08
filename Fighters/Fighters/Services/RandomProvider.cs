namespace Fighters.Services;

public class RandomProvider : IRandomProvider
{
    private readonly Random _random = new();

    public int Next( int minValue, int maxValue )
    {
        return _random.Next( minValue, maxValue );
    }

    public double NextDouble()
    {
        return _random.NextDouble();
    }
}