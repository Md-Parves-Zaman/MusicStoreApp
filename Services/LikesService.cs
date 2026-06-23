using MusicStoreApp.Utils;
namespace MusicStoreApp.Services;
public class LikesService
{
    public int Generate(double avg, SeededRandom rng)
    {
        int baseVal = (int)Math.Floor(avg);
        double frac = avg - baseVal;

        int likes = baseVal;

        if (rng.NextDouble() < frac)
            likes++;

        return likes;
    }
}