using Microsoft.AspNetCore.Mvc;
using MusicStoreApp.Services;

namespace MusicStoreApp.Controllers;

[ApiController]
[Route("api/songs")]
public class SongsController : ControllerBase
{
    private readonly SongGeneratorService _gen;
    private readonly AudioService _audio;

    public SongsController(SongGeneratorService gen, AudioService audio)
    {
        _gen = gen;
        _audio = audio;
    }

    [HttpGet]
    public IActionResult Get(
        int page = 1,
        int pageSize = 20,
        int seed = 123,
        double likes = 3.5,
        string lang = "en-US")
    {
        var list = new List<object>();

        int start = (page - 1) * pageSize + 1;

        for (int i = 0; i < pageSize; i++)
        {
            var song = _gen.Generate(seed + page, start + i, lang, likes);
            list.Add(song);
        }

        return Ok(new { page, items = list });
    }

    [HttpGet("{id}/audio")]
    public IActionResult Audio(int id, int seed = 123)
    {
        var audio = _audio.Generate(seed + id);
        return File(audio, "application/octet-stream");
    }
}