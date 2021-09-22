using Cobalt.Api.Wrapper;

namespace CobaltTShock.Wrapper
{
    public class TShockPosition : ICobaltPosition
    {
        public float X { get; }
        public float Y { get; }

        public string ToPrettyString => $"[ x:{X} | y:{Y} ]";
        
        public TShockPosition(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}