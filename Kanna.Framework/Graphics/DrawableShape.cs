using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

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
        public Axes RelativePositionAxes { get; set; } = Axes.None;
        public Anchor Anchor { get; set; } = Anchor.Centre;
        public Anchor Origin { get; set; } = Anchor.Centre;

        private float[] vertices = new float[4];

        public virtual void OnRenderFrame(FrameEventArgs e, Vector2i windowSize)
        {
            int windowWidth = windowSize.X;
            int windowHeight = windowSize.Y;
        }
    }
}
