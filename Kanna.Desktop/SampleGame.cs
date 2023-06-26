using Kanna.Framework;
using Kanna.Framework.Audio;

namespace Kanna.Desktop
{
    public class SampleGame : Game
    {
        public SampleGame()
        {
            // TODO: Change project name to SampleGame and seperate repo
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            Track track = new Track("Resources/audio.mp3");
            track.Play();
        }
    }
}
