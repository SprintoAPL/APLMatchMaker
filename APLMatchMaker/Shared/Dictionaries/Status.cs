using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLMatchMaker.Shared.Dictionaries
{
    public static class Status
    {
        public static Dictionary<int, string> Statuses { get; } = new Dictionary<int, string> {
            {1, "APL sök pågår"},
            {2, "APL klar"},
            {3, "Avbruten"},
            {4, "Anställd innan utbildnings slut"},
            {5, "Anställd efter utbildnings slut"},
            {6, "Övrigt"}
        };
    }
}
