using System.Collections.Generic;
using Cobalt.Api.Message;

namespace Cobalt.Api.Model
{
    public interface IPrettyPrintable
    {
        string ToString();

        string ToString(FormattedDictionary formatter);

        IEnumerable<string> ToStringList();

        IEnumerable<string> ToStringList(FormattedDictionary formatter);

        string ToPrettyString();

        string ToPrettyString(FormattedDictionary formatter);

        IEnumerable<string> ToPrettyStringList();

        IEnumerable<string> ToPrettyStringList(FormattedDictionary formatter);
    }
}