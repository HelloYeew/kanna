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
            Bass.DeviceNonStop = true;

            if (!Bass.Init())
            {
                Logger.Log("Cannot initialize BASS");
            }
            else
            {
                Logger.Log("BASS Initialized.");
                Logger.Log($"BASS Version: {Bass.Version}");
                Logger.Log($"BASS FX Version: {BassFx.Version}");
                Logger.Log($"BASS Mix Version: {BassMix.Version}");
            }
        }
    }
}
