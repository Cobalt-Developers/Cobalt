namespace Cobalt.Api.Commands.Arguments
{
    public abstract class ArgumentConstraint
    {
        public abstract bool IsSatisfied(string input);

        public abstract string GetErrorMessage(string input);
    }
}