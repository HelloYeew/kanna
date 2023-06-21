using System.Reflection;
using ManagedBass;
using OpenTK.Windowing.Desktop;

namespace Kanna.Framework
{
    public class Game : GameWindow
    {
        public Game( int width = 1366, int height = 768, string title = "Kanna Framework") : base(GameWindowSettings.Default,
            new NativeWindowSettings() {Size = (width, height), Title = title})
        {
            Bass.Init();
            Title = "Kanna Framework (Running " + Assembly.GetEntryAssembly()?.GetName().Name + ")";
            Console.WriteLine("Initialized Bass version " + Bass.Version + " with device " + Bass.CurrentDevice);
        }
    }
}
