using Microsoft.Xna.Framework;

namespace CobaltCore.Helpers
{
    public class ColorHelper
    {
        public static uint AsRgb(Color color)
        {
            return ((color.packedValue & 0x000000FF) << 16) | (color.packedValue & 0x0000FF00) | ((color.packedValue & 0x00FF0000) >> 16);
        }
    }
}