using System.Reflection;
using Kanna.Framework.Audio;
using Kanna.Framework.Graphics;
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
        public List<IDrawable> Drawables = new List<IDrawable>();
        private List<DrawableVertices> drawableVerticesList = new List<DrawableVertices>();

        public FPSMode FpsMode = FPSMode.DoubleMultiplier;

        // TODO: This should be reflect the real monitor refresh rate
        public int MonitorRefreshRate = 120;

        private uint[] vertexBuffer = Array.Empty<uint>();
        private uint[] vertexArray = Array.Empty<uint>();

        private Shader? shapeShader;

        public Game(int width = 1366, int height = 768, string title = "") : base(GameWindowSettings.Default,
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

            for (int i = 0; i < Drawables.Count; i++)
            {
                for (int j = 0; j < Drawables[i].Vertices.Count; j++)
                {
                    drawableVerticesList.Add(new DrawableVertices
                    {
                        Drawable = Drawables[i],
                        Vertices = Drawables[i].Vertices[j]
                    });
                }
            }

            vertexArray = new uint[drawableVerticesList.Count];
            vertexBuffer = new uint[drawableVerticesList.Count];
            GL.GenVertexArrays(drawableVerticesList.Count, vertexArray);
            GL.GenBuffers(drawableVerticesList.Count, vertexBuffer);

            for (int i = 0; i < drawableVerticesList.Count; i++)
            {
                GL.BindVertexArray(vertexArray[i]);
                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer[i]);
                GL.BufferData(BufferTarget.ArrayBuffer, drawableVerticesList[i].Vertices.Length * sizeof(float), drawableVerticesList[i].Vertices, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(0, drawableVerticesList[i].Vertices.Length/3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);
            }

            // Unbind every VAO for safety
            GL.BindVertexArray(0);

            shapeShader = new Shader("Resources/Shaders/sh_shape.vert", "Resources/Shaders/sh_shape.frag");
            shapeShader.Use();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            shapeShader?.Use();

            int vertexColorLocation = GL.GetUniformLocation(shapeShader.Handle, "colorVar");

            int vertexArrayIndex = 0;
            for (int i = 0; i < drawableVerticesList.Count; i++)
            {
                GL.Uniform4(vertexColorLocation, drawableVerticesList[i].Drawable.Color.R, drawableVerticesList[i].Drawable.Color.G, drawableVerticesList[i].Drawable.Color.B, drawableVerticesList[i].Drawable.Color.A);

                GL.BindVertexArray(vertexArray[vertexArrayIndex]);
                vertexArrayIndex++;
                GL.DrawArrays(PrimitiveType.Triangles, 0, drawableVerticesList[i].Vertices.Length/3);
            }

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
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
                switch (FpsMode)
                {
                    case FPSMode.DoubleMultiplier:
                        FpsMode = FPSMode.FourMultiplier;
                        UpdateFrequency = MonitorRefreshRate * 4;
                        break;

                    case FPSMode.FourMultiplier:
                        FpsMode = FPSMode.EightMultiplier;
                        UpdateFrequency = MonitorRefreshRate * 8;
                        break;

                    case FPSMode.EightMultiplier:
                        FpsMode = FPSMode.Unlimited;
                        UpdateFrequency = 0;
                        break;

                    case FPSMode.Unlimited:
                        FpsMode = FPSMode.VSync;
                        UpdateFrequency = MonitorRefreshRate;
                        VSync = VSyncMode.On;
                        break;

                    case FPSMode.VSync:
                        FpsMode = FPSMode.DoubleMultiplier;
                        UpdateFrequency = MonitorRefreshRate * 2;
                        VSync = VSyncMode.Off;
                        break;
                }

                Logger.Log($"FPS Mode changed to {FpsMode}");
            }
        }

        protected override void OnFocusedChanged(FocusedChangedEventArgs e)
        {
            base.OnFocusedChanged(e);

            if (e.IsFocused)
                switch (FpsMode)
                {
                    case FPSMode.DoubleMultiplier:
                        UpdateFrequency = MonitorRefreshRate * 2;
                        break;

                    case FPSMode.FourMultiplier:
                        UpdateFrequency = MonitorRefreshRate * 4;
                        break;

                    case FPSMode.EightMultiplier:
                        UpdateFrequency = MonitorRefreshRate * 8;
                        break;

                    case FPSMode.Unlimited:
                        UpdateFrequency = 0;
                        break;

                    case FPSMode.VSync:
                        UpdateFrequency = MonitorRefreshRate;
                        break;
                }
            else
                // Limit FPS to monitor refresh rate
                UpdateFrequency = MonitorRefreshRate;
        }
    }

    internal class DrawableVertices
    {
        public float[] Vertices;
        public IDrawable Drawable;
    }
}
