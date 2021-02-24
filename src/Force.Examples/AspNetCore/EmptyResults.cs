using System.Collections.Generic;

namespace Force.Examples.AspNetCore
{
    internal class EmptyResults
    {
        internal static readonly Dictionary<string, IEnumerable<string>> EmptyErrors = 
            new Dictionary<string, IEnumerable<string>>();
    }
}