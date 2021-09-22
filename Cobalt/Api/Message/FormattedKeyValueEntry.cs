namespace Cobalt.Api.Message
{
    public class FormattedKeyValue
    {
        private readonly string _formatter;

        public FormattedKeyValue(string formatter)
        {
            _formatter = formatter;
        }

        public string ToString(object key, object value)
        {
            return ToString(key.ToString(), value.ToString());
        }
        
        public string ToString(string key, string value)
        {
            return _formatter.Replace("{{key}}", key).Replace("{{value}}", value);
        }
    }
}