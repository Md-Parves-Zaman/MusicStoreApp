namespace MusicStoreApp.Utils;

public class SeededRandom
{
    private Random _random;

    public SeededRandom(int seed)
    {
        _random = new Random(seed);
    }

    public int Next(int min, int max)
    {
        return _random.Next(min, max);
    }

    public double NextDouble()
    {
        return _random.NextDouble();
    }
}