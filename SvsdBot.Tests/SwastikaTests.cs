﻿namespace SvsdBot.Tests
{
    [TestClass]
    public class SwastikaTests
    {
        [TestMethod]
        public void EvenSvastikaTest()
        {
            BotTextGenerator generator = new();
            string word = "Жопа";
            string result =
                "Ж  АПОЖ" + "\n" +
                "О  П" + "\n" +
                "П  О" + "\n" +
                "АПОЖОПА" + "\n" +
                "   О  П" + "\n" +
                "   П  О" + "\n" +
                "ЖОПА  Ж";
            string generated = generator.GetSwastika(word);
            Assert.AreEqual(result, generated);
        }

        [TestMethod]
        public void OddSvastikaTest()
        {
            BotTextGenerator generator = new();
            string word = "Пук";
            string result =
                "П КУП" + "\n" +
                "У У" + "\n" +
                "КУПУК" + "\n" +
                "  У У" + "\n" +
                "ПУК П";
            string generated = generator.GetSwastika(word);
            Assert.AreEqual(result, generated);
        }

        [TestMethod]
        public void TooSmallSvastikaTest()
        {
            BotTextGenerator generator = new();
            string word = "АА";
            string result = string.Empty;
            string generated = generator.GetSwastika(word);
            Assert.AreEqual(result, generated);
        }

        [TestMethod]
        public void TooLongSvastikaTest()
        {
            BotTextGenerator generator = new();

            // 26 characters
            string word = "12345678901234567890123456"; 

            // swaston made from 25 characters
            string result =                 
                "1                       5432109876543210987654321\n" +
                "2                       4\n" +
                "3                       3\n" +
                "4                       2\n" +
                "5                       1\n" +
                "6                       0\n" +
                "7                       9\n" +
                "8                       8\n" +
                "9                       7\n" +
                "0                       6\n" +
                "1                       5\n" +
                "2                       4\n" +
                "3                       3\n" +
                "4                       2\n" +
                "5                       1\n" +
                "6                       0\n" +
                "7                       9\n" +
                "8                       8\n" +
                "9                       7\n" +
                "0                       6\n" +
                "1                       5\n" +
                "2                       4\n" +
                "3                       3\n" +
                "4                       2\n" +
                "5432109876543210987654321234567890123456789012345\n" +
                "                        2                       4\n" +
                "                        3                       3\n" +
                "                        4                       2\n" +
                "                        5                       1\n" +
                "                        6                       0\n" +
                "                        7                       9\n" +
                "                        8                       8\n" +
                "                        9                       7\n" +
                "                        0                       6\n" +
                "                        1                       5\n" +
                "                        2                       4\n" +
                "                        3                       3\n" +
                "                        4                       2\n" +
                "                        5                       1\n" +
                "                        6                       0\n" +
                "                        7                       9\n" +
                "                        8                       8\n" +
                "                        9                       7\n" +
                "                        0                       6\n" +
                "                        1                       5\n" +
                "                        2                       4\n" +
                "                        3                       3\n" +
                "                        4                       2\n" +
                "1234567890123456789012345                       1";

            string generated = generator.GetSwastika(word);
            Assert.AreEqual(result, generated);
        }
    }
}