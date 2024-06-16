﻿using System.Text;

namespace SvsdBot
{
    /// <summary>
    /// Generates swastons an possibly other things.
    /// </summary>
    public class BotTextGenerator
    {
        private const int CHARACTERS_LIMIT = 35;

        /// <summary>
        /// Returns swaston.
        /// </summary>
        /// <param name="word">Input word.</param>
        /// <returns>Word in the swaston form. Or <c>empty string</c> if swaston creation is impossible.</returns>
        public string GetSwastika(string word)
        {
            word = word.ToUpper();
            int n = word.Length;
            if (n < 3)
            {
                return string.Empty;
            }

            if (n > CHARACTERS_LIMIT)
            {
                word = word[0..CHARACTERS_LIMIT];
                n = CHARACTERS_LIMIT;
            }

            int size = (2 * n) - 1;
            char[,] swastika = new char[size, size];

            // Fill with spaces
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    swastika[i, j] = ' ';
                }
            }

            // Top-left to bottom-left
            for (int i = 0; i < n; i++)
            {
                swastika[i, 0] = word[i];
                swastika[size - i - 1, size - 1] = word[i];
            }

            // Top-left to top-right
            for (int i = 0; i < n; i++)
            {
                swastika[0, size - i - 1] = word[i];
                swastika[size - 1, i] = word[i];
            }

            // Center horizontal line
            for (int i = n; i < size; i++)
            {
                swastika[n - 1, i - 1] = word[i - n];
                swastika[n - 1, size - i] = word[i - n];
            }

            // Center vertical line
            for (int i = n; i < size; i++)
            {
                swastika[i - 1, n - 1] = word[i - n];
                swastika[size - i, n - 1] = word[i - n];
            }

            return ConvertSwastikaToString(swastika);
        }

        private static string ConvertSwastikaToString(char[,] swastika)
        {
            int size = swastika.GetLength(0);
            StringBuilder result = new ();

            for (int i = 0; i < size; i++)
            {
                StringBuilder row = new ();
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