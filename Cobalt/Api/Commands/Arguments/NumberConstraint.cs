using System;

namespace Cobalt.Api.Commands.Arguments
{
    public class NumberConstraint : ArgumentConstraint
    {
        private readonly long lowerLimit;
        private readonly long upperLimit;

        public NumberConstraint()
        {
            lowerLimit = int.MinValue;
            upperLimit = int.MaxValue;
        }

        public NumberConstraint(long lowerLimit, long upperLimit)
        {
            if (upperLimit < lowerLimit) throw new ArgumentException("lowerValue is higher than upperValue");
            
            this.lowerLimit = lowerLimit;
            this.upperLimit = upperLimit;
        }

        public override bool IsSatisfied(string input)
        {
            if (!long.TryParse(input, out var parsed)) return false;
            return parsed >= lowerLimit && parsed <= upperLimit;
        }

        public override string GetErrorMessage(string input)
        {
            return $"'{input}' is not a number between '{lowerLimit}' and '{upperLimit}'.";
        }
    }
}