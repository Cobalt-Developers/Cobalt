using Cobalt.Api.Wrappers;

namespace Cobalt.Api.Commands.Arguments
{
    public class ArgumentI
    {
        private ICobaltPlugin Plugin { get; }

        public string Placeholder { get; }
        public bool Optional { get; } 
        public ArgumentConstraint Constraint { get; }

        public ArgumentI(ICobaltPlugin plugin, string placeholder, bool optional, ArgumentConstraint constraint)
        {
            Plugin = plugin;
            Placeholder = placeholder;
            Optional = optional;
            Constraint = constraint;
        }

        public bool TestArgumentOrError(ICobaltPlayer argsPlayer, string input)
        {
            if (Constraint == null || Constraint.IsSatisfied(input)) return true;
            argsPlayer.SendErrorMessage("Argument Constraint not satisfied:");
            argsPlayer.SendErrorMessage(Constraint.GetErrorMessage(input));
            return false;
        }
        
        public string ToPrettyString()
        {
            return Optional ? $"[{Placeholder}]" : $"<{Placeholder}>";
        }
    }
}