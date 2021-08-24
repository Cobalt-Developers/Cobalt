using System;

namespace CobaltCore.Commands.Arguments
{
    public class IntegerConstraint : ArgumentConstraint
    {
        private readonly int lowerLimit;
        private readonly int upperLimit;

        public IntegerConstraint()
        {
            lowerLimit = int.MinValue;
            upperLimit = int.MaxValue;
        }

        public IntegerConstraint(int lowerLimit, int upperLimit)
        {
            if (upperLimit > lowerLimit) throw new ArgumentException("lowerValue is higher than upperValue");
            
            this.lowerLimit = lowerLimit;
            this.upperLimit = upperLimit;
        }

        public override bool IsSatisfied(string input)
        {
            if (!int.TryParse(input, out var parsed)) return false;
            return parsed >= lowerLimit && parsed <= upperLimit;
        }

        public override string GetErrorMessage(string input)
        {
            return $"'{input}' is not a number between '{lowerLimit}' and '{upperLimit}'.";
        }
    }
}