using CobaltCore.Helpers;
using Microsoft.Xna.Framework;

namespace CobaltCore.Messages
{
    public class ColorScheme
    {
        public int Primary { get; }
        public int Secondary { get; }
        public int Text { get; }
        public int Misc { get; }

        public ColorScheme(int primary, int secondary, int text, int misc)
        {
            Primary = primary;
            Secondary = secondary;
            Text = text;
            Misc = misc;
        }

        public ColorScheme(Color primary, Color secondary, Color text, Color misc)
        {
            Primary = (int) ColorHelper.AsRgb(primary);
            Secondary = (int) ColorHelper.AsRgb(secondary);
            Text = (int) ColorHelper.AsRgb(text);
            Misc = (int) ColorHelper.AsRgb(misc);
        }
    }
}