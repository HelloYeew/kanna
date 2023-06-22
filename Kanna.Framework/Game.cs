using System.Reflection;
using Kanna.Framework.Audio;
using Kanna.Framework.Logging;
using OpenTK.Windowing.Desktop;

namespace Kanna.Framework
{
    public class Game : GameWindow
    {
        public Game( int width = 1366, int height = 768, string title = "") : base(GameWindowSettings.Default,
            new NativeWindowSettings() {Size = (width, height), Title = title})
        {
            if (title == "")
                Title = "Kanna Framework (Running " + Assembly.GetEntryAssembly()?.GetName().Name + ")";

            Logger.AddHeader();
            AudioManager.InitBass();
        }
    }
}
