namespace MusicStoreApp.Utils;

public static class SeedHelper
{
    public static int Combine(int seed, int page)
    {
        return HashCode.Combine(seed, page);
    }
}