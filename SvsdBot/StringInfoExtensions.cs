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

            int n = input.LengthInTextElements;
            StringBuilder reversedString = new();
            for (int i = n; i > 0; i--)
            {
                string element = input.SubstringByTextElements(i - 1, 1);
                reversedString.Append(element);
            }

            return reversedString.ToString();
        }
    }
}