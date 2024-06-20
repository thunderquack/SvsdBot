using System.Globalization;
using System.Text;

namespace SvsdBot
{
    /// <summary>
    /// Extension for the Unicode string reversion.
    /// </summary>
    public static class StringInfoExtensions
    {
        /// <summary>
        /// Reverse string.
        /// </summary>
        /// <param name="input">Stringinfo object.</param>
        /// <returns>Reversed string.</returns>
        public static string ReverseStringInfo(this StringInfo input)
        {
            if (input == null)
            {
                return string.Empty;
            }

            StringInfo stringInfo = new StringInfo(input.ToString());
            int n = stringInfo.LengthInTextElements;
            StringBuilder reversedString = new();
            for (int i = n; i > 0; i--)
            {
                reversedString.Append(stringInfo.SubstringByTextElements(i - 1, 1));
            }

            return reversedString.ToString();
        }
    }
}