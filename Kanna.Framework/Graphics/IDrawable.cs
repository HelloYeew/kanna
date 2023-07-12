using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace Kanna.Framework.Graphics
{
    /// <summary>
    /// The interface for drawable objects that can be drawn on the screen.
    /// </summary>
    public interface IDrawable
    {
        public List<float[]> Vertices { get; set; }
        public Color4 Color { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public Axes RelativeSizeAxes { get; set; }
        public Anchor Anchor { get; set; }
        public Anchor Origin { get; set; }
        public void OnRenderFrame(FrameEventArgs e, Vector2i windowSize);
    }
}
