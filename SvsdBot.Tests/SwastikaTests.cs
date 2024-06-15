namespace SvsdBot.Tests
{
    [TestClass]
    public class SwastikaTests
    {
        [TestMethod]
        public void EvenSvastikaTest()
        {
            BotTextGenerator generator = new ();
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
    }
}