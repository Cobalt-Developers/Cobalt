using System;

namespace CobaltCore.Commands.Arguments
{
    public class LengthConstraint : ArgumentConstraint
    {
        private readonly int lowerLimit;
        private readonly int upperLimit;

        public LengthConstraint()
        {
            lowerLimit = int.MinValue;
            upperLimit = int.MaxValue;
        }

        public LengthConstraint(int lowerLimit, int upperLimit)
        {
            if (upperLimit > lowerLimit) throw new ArgumentException("lowerValue is higher than upperValue");
            
            this.lowerLimit = lowerLimit;
            this.upperLimit = upperLimit;
        }

        public override bool IsSatisfied(string input)
        {
            var parsed = input.Length;
            return parsed >= lowerLimit && parsed <= upperLimit;
        }

        public override string GetErrorMessage(string input)
        {
            return $"'{input}' needs to be between '{lowerLimit}' and '{upperLimit}' characters long.";
        }
    }
}