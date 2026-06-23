namespace MusicStoreApp.Models;

public class SongDetailDto : SongDto
{
    public string Review { get; set; }
    public string CoverText { get; set; }
    public string AudioBase64 { get; set; }
}