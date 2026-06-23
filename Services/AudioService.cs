using MusicStoreApp.Utils;

namespace MusicStoreApp.Services;

public class AudioService
{
    public byte[] Generate(int seed)
    {
        var rng = new SeededRandom(seed);

        int sampleRate = 44100;
        int duration = 3;

        MemoryStream ms = new();
        BinaryWriter writer = new(ms);

        int samples = sampleRate * duration;

        for (int i = 0; i < samples; i++)
        {
            double freq = 220 + rng.Next(0, 400);
            double t = (double)i / sampleRate;

            short value = (short)(Math.Sin(2 * Math.PI * freq * t) * 30000);

            writer.Write(value);
        }

        return ms.ToArray();
    }
}