using MusicStoreApp.Models;
using MusicStoreApp.Utils;

namespace MusicStoreApp.Services;

public class SongGeneratorService
{
    private readonly LocalizationService _loc;
    private readonly LikesService _likes;

    public SongGeneratorService(LocalizationService loc, LikesService likes)
    {
        _loc = loc;
        _likes = likes;
    }

    public SongDto Generate(int seed, int index, string lang, double avgLikes)
    {
        var rng = new SeededRandom(seed + index);

        return new SongDto
        {
            Index = index,
            Title = _loc.GetRandom(lang, "titles", rng),
            Artist = _loc.GetRandom(lang, "artists", rng),
            Album = _loc.GetRandom(lang, "albums", rng),
            Genre = _loc.GetRandom(lang, "genres", rng),
            Likes = _likes.Generate(avgLikes, rng)
        };
    }
}