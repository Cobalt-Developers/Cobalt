using System.Collections.Generic;
using Cobalt.Api.Message;

namespace Cobalt.Api.Model
{
    public abstract class WrappedEntity : IPrettyPrintable
    {
        public override string ToString()
        {
            return ToString(new FormattedDictionary("{{key}}: {{value}}", "[ ", " ]", " | "));
        }
        
        public string ToString(FormattedDictionary formatter)
        {
            return formatter.ToString(GetVariables());
        }
        
        public IEnumerable<string> ToStringList()
        {
            return ToStringList(new FormattedDictionary("{{key}}: {{value}}"));
        }
        
        public IEnumerable<string> ToStringList(FormattedDictionary formatter)
        {
            return formatter.ToList(GetVariables());
        }
        
        public string ToPrettyString()
        {
            return ToPrettyString(new FormattedDictionary("{{key}}: {{value}}", "[ ", " ]", " | "));
        }
        
        public string ToPrettyString(FormattedDictionary formatter)
        {
            return formatter.ToString(GetPrintableVariables());
        }
        
        public IEnumerable<string> ToPrettyStringList()
        {
            return ToPrettyStringList(new FormattedDictionary("{{key}}: {{value}}"));
        }
        
        public IEnumerable<string> ToPrettyStringList(FormattedDictionary formatter)
        {
            return formatter.ToList(GetPrintableVariables());
        }

        protected abstract Dictionary<object, object> GetPrintableVariables();

        private Dictionary<object, object> GetVariables()
        {
            Dictionary<object, object> variables = new Dictionary<object, object>();
            foreach (var fieldInfo in GetType().GetFields())
            {
                variables.Add(fieldInfo.Name.ToLower(), fieldInfo.GetValue(this));
            }
            foreach (var propertyInfo in GetType().GetProperties())
            {
                variables.Add(propertyInfo.Name.ToLower(), propertyInfo.GetValue(this));
            }
            return variables;
        }
    }
}