using OpenTK.Mathematics;

namespace Kanna.Framework.Graphics.Shape
{
    public class Triangle : IDrawable
    {
        public float[] Vertices { get; set; }
        public Color4 Color { get; set; }
    }
}
