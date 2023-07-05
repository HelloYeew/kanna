using Kanna.Framework.Logging;
using ManagedBass;

namespace Kanna.Framework.Audio
{
    public class Track
    {
        private readonly int fileStream;

        /// <summary>
        /// Create a new stream of audio file for play.
        /// </summary>
        /// <param name="filePath">File path to file</param>
        public Track(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Logger.Log("File not exists");
            }

            fileStream = Bass.CreateStream(filePath);
            Logger.Log($"Initialized stream for {filePath} with {fileStream}", LoggingTarget.Runtime, LogLevel.Debug);
        }

        /// <summary>
        /// Play the track
        /// </summary>
        public void Play()
        {
            Bass.ChannelPlay(fileStream);
        }

        /// <summary>
        /// Make this track play loop
        /// </summary>
        public void Loop()
        {
            Bass.ChannelFlags(fileStream, BassFlags.Loop, BassFlags.Loop);
        }

        /// <summary>
        /// Stop the track
        /// </summary>
        public void Stop()
        {
            Bass.ChannelStop(fileStream);
        }

        /// <summary>
        /// Get the amplitude of the track
        /// </summary>
        /// <returns>Current amplitude of the track that's divided by 32768 (max value of short)</returns>
        public float Amplitude()
        {
            return Bass.ChannelGetLevel(fileStream) / 32768f;
        }
    }
}
