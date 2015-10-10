using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Common.Extensions
{
    public static class StringExtensions
    {
        public static Int32 ToInt(this String theString, Int32 defaultNumber = 0)
        {
            Int32 number;
            var success = Int32.TryParse(theString, out number);
            return success ? number : defaultNumber;
        }

        public static Boolean ToBool(this String theString)
        {
            Boolean value;
            var success = Boolean.TryParse(theString, out value);
            return success && value;
        }

        public static Boolean ContainsAny(this String theString, params String[] tokens)
        {
            var lowerCaseString = theString.ToLower();
            return tokens.Any(lowerCaseString.Contains);
        }
    }
}
