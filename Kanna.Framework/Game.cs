using System.Diagnostics;
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
        private readonly float[] _vertices =
        {
            -0.5f, -0.5f, 0.0f, // Bottom-left vertex
            0.5f, -0.5f, 0.0f, // Bottom-right vertex
            0.0f,  0.5f, 0.0f  // Top vertex
        };

        private readonly float[] _vertices2 =
        {
            -0.25f, -0.25f, 0.5f, // Bottom-left vertex
            0.25f, -0.25f, 0.5f, // Bottom-right vertex
            0.5f,  0.25f, 0.5f  // Top vertex
        };


        public FPSMode FpsMode = FPSMode.DoubleMultiplier;

        // TODO: This should be reflect the real monitor refresh rate
        public int MonitorRefreshRate = 120;

        private uint[] _vertexBuffer, _vertexArray, _elementBuffer;

        private Shader _shapeShader;

        private Stopwatch _timer;

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

            _vertexArray = new uint[2];
            _vertexBuffer = new uint[2];
            GL.GenVertexArrays(2, _vertexArray);
            GL.GenBuffers(2, _vertexBuffer);

            GL.BindVertexArray(_vertexArray[0]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer[0]);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindVertexArray(_vertexArray[1]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer[1]);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices2.Length * sizeof(float), _vertices2, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Unbind every VAO for safety
            GL.BindVertexArray(0);

            _shapeShader = new Shader("Resources/Shaders/sh_shape.vert", "Resources/Shaders/sh_shape.frag");
            _shapeShader.Use();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            _shapeShader.Use();

            int vertexColorLocation = GL.GetUniformLocation(_shapeShader.Handle, "colorVar");
            GL.Uniform4(vertexColorLocation, 0.0f, 1.0f, 0.5f, 1.0f);

            GL.BindVertexArray(_vertexArray[0]);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            GL.Uniform4(vertexColorLocation, 1.0f, 1.0f, 0.5f, 1.0f);

            GL.BindVertexArray(_vertexArray[1]);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

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
}
