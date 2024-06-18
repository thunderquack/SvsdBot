using System.Globalization;
using System.Text;

namespace SvsdBot
{
    /// <summary>
    /// Generates swastons an possibly other things.
    /// </summary>
    public class BotTextGenerator
    {
        private const int CHARACTERS_LIMIT = 25;

        /// <summary>
        /// Returns swaston.
        /// </summary>
        /// <param name="word">Input word.</param>
        /// <returns>Word in the swaston form. Or <c>empty string</c> if swaston creation is impossible.</returns>
        public string GetSwastika(string word)
        {
            word = word.Split(" ").Last();
            word = word.ToUpper();
            StringInfo stringInfo = new StringInfo(word);
            int len = word.Length;
            int n = stringInfo.LengthInTextElements;

            bool unicodeMode = len != n;

            if (n < 3)
            {
                return string.Empty;
            }

            if (n > CHARACTERS_LIMIT)
            {
                word = word[0..CHARACTERS_LIMIT];
                n = CHARACTERS_LIMIT;
            }

            stringInfo = new StringInfo(word);

            int size = (2 * n) - 1;
            string[,] swastika = new string[size, size];

            // Fill with spaces
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    swastika[i, j] = unicodeMode ? "⬜️" : " ";
                }
            }

            // Top-left to bottom-right
            for (int i = 0; i < n; i++)
            {
                swastika[i, 0] = stringInfo.SubstringByTextElements(i, 1);
                swastika[size - i - 1, size - 1] = stringInfo.SubstringByTextElements(i, 1);
            }

            // Top-right to bottom-left
            for (int i = 0; i < n; i++)
            {
                swastika[0, size - i - 1] = stringInfo.SubstringByTextElements(i, 1);
                swastika[size - 1, i] = stringInfo.SubstringByTextElements(i, 1);
            }

            // Center horizontal line
            for (int i = n; i < size; i++)
            {
                swastika[n - 1, i - 1] = stringInfo.SubstringByTextElements(i - n, 1);
                swastika[n - 1, size - i] = stringInfo.SubstringByTextElements(i - n, 1);
            }

            // Center vertical line
            for (int i = n; i < size; i++)
            {
                swastika[i - 1, n - 1] = stringInfo.SubstringByTextElements(i - n, 1);
                swastika[size - i, n - 1] = stringInfo.SubstringByTextElements(i - n, 1);
            }

            return ConvertSwastikaToString(swastika);
        }

        private static string ConvertSwastikaToString(string[,] swastika)
        {
            int size = swastika.GetLength(0);
            StringBuilder result = new();

            for (int i = 0; i < size; i++)
            {
                StringBuilder row = new();
                for (int j = 0; j < size; j++)
                {
                    row.Append(swastika[i, j]);
                }

                result.Append(row.ToString().TrimEnd());

                if (i < size - 1)
                {
                    result.Append('\n');
                }
            }

            return result.ToString();
        }
    }
}