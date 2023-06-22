using Kanna.Framework.Logging;
using ManagedBass;
using ManagedBass.Fx;
using ManagedBass.Mix;

namespace Kanna.Framework.Audio
{
    public class AudioManager
    {
        /// <summary>
        /// Initializes BASS.
        /// </summary>
        public static void InitBass()
        {
            // Reduce latency
            Bass.DeviceBufferLength = 10;
            Bass.PlaybackBufferLength = 100;

            Bass.DeviceNonStop = true;

            Bass.Configure((ManagedBass.Configuration)70, false);

            Bass.Init();

            Logger.Log("BASS Initialized.");
            Logger.Log($"BASS Version: {Bass.Version}");
            Logger.Log($"BASS FX Version: {BassFx.Version}");
            Logger.Log($"BASS Mix Version: {BassMix.Version}");
        }
    }
}
