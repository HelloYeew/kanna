using OpenTK.Mathematics;

namespace Kanna.Framework.Graphics.Utility
{
    /// <summary>
    /// Extension methods for <see cref="Color4"/> objects.
    /// </summary>
    public class Color4Extensions
    {
        /// <summary>
        /// Converts a hex string to a <see cref="Color4"/> object.
        /// </summary>
        /// <param name="hex">Color in hex format.</param>
        /// <returns><see cref="Color4"/> object.</returns>
        public static Color4 FromHex(string hex)
        {
            if (hex[0] == '#')
            {
                hex = hex.Substring(1);
            }

            return hex.Length switch
            {
                6 => new Color4(byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber), 255),
                8 => new Color4(byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber)),
                _ => throw new InvalidCastException("Invalid hex string")
            };
        }
    }
}
