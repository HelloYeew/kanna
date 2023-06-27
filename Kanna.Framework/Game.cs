using System.Reflection;
using Kanna.Framework.Audio;
using Kanna.Framework.Logging;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

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

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(Color4.MediumPurple);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            SwapBuffers();
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            // Control + F11 = Toggle Fullscreen
            if (e is {Control: true, Key: Keys.F11})
            {
                if (WindowState == WindowState.Fullscreen)
                    WindowState = WindowState.Normal;
                else
                    WindowState = WindowState.Fullscreen;
                Logger.Log($"Window state changed to {WindowState} ({ClientSize.X}x{ClientSize.Y})");
            }

            // Control + F12 = Toggle VSync
            // TODO: Add more FPS options
            if (e is {Control: true, Key: Keys.F12})
            {
                if (VSync == VSyncMode.On)
                    VSync = VSyncMode.Off;
                else
                    VSync = VSyncMode.On;
                Logger.Log($"VSync changed to {VSync} ({ClientSize.X}x{ClientSize.Y})");
            }
        }

        protected override void OnFocusedChanged(FocusedChangedEventArgs e)
        {
            base.OnFocusedChanged(e);

            if (e.IsFocused)
                // Don't limit FPS when focused
                UpdateFrequency = 0;
            else
                // Limit FPS to 60 when not focused
                UpdateFrequency = 60;
        }
    }
}
