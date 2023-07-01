using OpenTK.Mathematics;

namespace Kanna.Framework.Graphics
{
    /// <summary>
    /// The interface for drawable objects that can be drawn on the screen.
    /// </summary>
    public interface IDrawable
    {
        public float[] Vertices { get; set; }

        public Color4 Color { get; set; }
    }
}
