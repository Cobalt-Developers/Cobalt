using System;
using System.Collections.Generic;
using Cobalt.Api.Model;

namespace CobaltTShock.Wrapper
{
    public class TShockPosition : WrappedEntity, IPosition
    {
        public float X { get; }
        public float Y { get; }
        
        public TShockPosition(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        protected override Dictionary<object, object> GetPrintableVariables()
        {
            return new Dictionary<object, object>
            {
                {"x", X},
                {"y", Y}
            };
        }

        protected bool Equals(TShockPosition other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }
    }
}