using OpenTK.Mathematics;

namespace Kanna.Framework.Graphics
{
    public class Drawable : IDrawable
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
    }
}
