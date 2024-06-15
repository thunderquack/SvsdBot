namespace SvsdBot.Tests
{
    [TestClass]
    public class SvastikaTests
    {
        [TestMethod]
        public void TooShortSvastikaTest()
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
    }
}