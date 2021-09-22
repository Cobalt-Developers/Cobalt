using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cobalt.Api.Message
{
    public class FormattedDictionary
    {
        private readonly string _entryFormatter;
        private readonly string _prefix;
        private readonly string _postfix;
        private readonly string _separator;

        public FormattedDictionary(string entryFormatter, string prefix = "", string postfix = "", string separator = "")
        {
            _entryFormatter = entryFormatter;
            _prefix = prefix;
            _postfix = postfix;
            _separator = separator;
        }

        public string ToString(Dictionary<object, object> dictionary)
        {
            Dictionary<string, string> castDictionary = GetEntries(dictionary).ToDictionary(e => e.Key.ToString(), e => e.Value.ToString());
            return ToString(castDictionary);
        }

        public string ToString(Dictionary<string, string> dictionary)
        {
            var res = "";
            res += _prefix;
            res += string.Join(_separator,
                GetEntries(dictionary).Select(e =>
                    new FormattedKeyValue(_entryFormatter).ToString((string) e.Key, (string) e.Value)).ToArray());
            res += _postfix;
            return res;
        }

        public List<string> ToList(Dictionary<object, object> dictionary)
        {
            Dictionary<string, string> castDictionary = GetEntries(dictionary).ToDictionary(e => e.Key.ToString(), e => e.Value.ToString());
            return ToList(castDictionary);
        }
        
        public List<string> ToList(Dictionary<string, string> dictionary)
        {
            List<string> res = new List<string>();
            if (!_prefix.Equals(string.Empty)) res.Add(_prefix);
            foreach (var (entry, i) in GetEntries(dictionary).Select((item, index) => (item, index)))
            {
                res.Add(new FormattedKeyValue(_entryFormatter).ToString((string) entry.Key, (string) entry.Value) + (i < dictionary.Count ? _separator : ""));
            }
            if (!_postfix.Equals(string.Empty)) res.Add(_postfix);
            return res;
        }

        private static IEnumerable<DictionaryEntry> GetEntries(IDictionary dictionary)
        {
            foreach (DictionaryEntry entry in dictionary)
            {
                yield return entry;
            }
        }
    }
}