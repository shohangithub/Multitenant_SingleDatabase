using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPermissions.BulkLoadServices.Concrete.Internal
{
    internal static class CommaDelimitedHandler
    {
        public static List<string> DecodeCommaDelimitedNameWithCheck(this string line, int charNum, Action<string, int> checkValid)
        {
            var trimmedNames = new List<string>();
            while (charNum < line.Length)
            {
                var foundName = "";
                var startOfName = charNum;
                while (charNum < line.Length && line[charNum] != ',')
                {
                    foundName += line[charNum];
                    charNum++;
                }
                if (foundName.Length > 0)
                {
                    var trimmedName = foundName.Trim();
                    checkValid(trimmedName, startOfName);
                    trimmedNames.Add(trimmedName);
                }
                charNum++;
            }

            return trimmedNames;
        }
    }
}
