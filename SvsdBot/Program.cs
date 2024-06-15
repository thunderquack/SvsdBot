using Telegram.Bot;

namespace SvsdBot
{
    /// <summary>
    /// Entry point.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            TelegramBotClient client = new (File.ReadAllText("apikey.txt"));
            Console.WriteLine(client.GetMeAsync().Result);
        }
    }
}