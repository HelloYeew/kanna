using OpenTK.Mathematics;

namespace Kanna.Framework.Graphics.Shape
{
    public class Triangle : IDrawable
    {
        public Triangle(float[] vertices)
        {
            Vertices = vertices;
        }

        public float[] Vertices { get; set; }
        public Color4 Color { get; set; }
    }
}
