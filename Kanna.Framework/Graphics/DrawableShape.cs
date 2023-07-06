using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Kanna.Framework.Graphics
{
    public class DrawableShape : IDrawable
    {
        public List<float[]> Vertices { get; set; }
        public Color4 Color { get; set; } = Color4.Black;
        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;
        public float Width { get; set; } = 0;
        public float Height { get; set; } = 0;
        public Axes RelativeSizeAxes { get; set; } = Axes.None;
        public Anchor Anchor { get; set; } = Anchor.Centre;
        public Anchor Origin { get; set; } = Anchor.Centre;

        private float[] vertices = new float[4];

        public virtual void OnRenderFrame(FrameEventArgs e, NativeWindow window)
        {
            int windowWidth = window.Size.X;
            int windowHeight = window.Size.Y;
            switch (Origin)
            {
                case Anchor.Centre:
                    switch (Anchor)
                    {
                        // Convert X and Y to the vertices
                        // Also determine the vertices' width and height of the shape and window
                        case Anchor.Centre:
                            switch (RelativeSizeAxes)
                            {

                            }

                    }

            }
        }
    }
}
