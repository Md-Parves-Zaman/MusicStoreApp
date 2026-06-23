using System.Text.Json;
using MusicStoreApp.Utils;
namespace MusicStoreApp.Services;

public class LocalizationService
{
    private readonly Dictionary<string, Dictionary<string, string[]>> _cache = new();

    public LocalizationService()
    {
        Load("en-US");
        Load("de-DE");
    }

    private void Load(string lang)
    {
        var path = Path.Combine("Localization", $"{lang}.json");
        var json = File.ReadAllText(path);
        _cache[lang] = JsonSerializer.Deserialize<Dictionary<string, string[]>>(json)
            ?? new Dictionary<string, string[]>();
    }

    public string GetRandom(string lang, string key, SeededRandom rng)
    {
        var list = _cache[lang][key];
        int index = rng.Next(0, list.Length);
        return list[index];
    }
}