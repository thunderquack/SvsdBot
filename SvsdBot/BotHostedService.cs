using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;

namespace SvsdBot
{
    /// <summary>
    /// Hosted service for the bot.
    /// </summary>
    public class BotHostedService(IOptions<BotConfiguration> botConfig, ILogger<BotHostedService> logger) : IHostedService
    {
        private readonly ILogger<BotHostedService> logger = logger;
        private readonly BotConfiguration botConfig = botConfig.Value;
        private readonly BotTextGenerator botTextGenerator = new();
        private ITelegramBotClient botClient;

        /// <summary>
        /// Starts service.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task.</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.botClient = new TelegramBotClient(this.botConfig.BotToken);

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>(),
                ThrowPendingUpdates = true,
            };

            this.botClient.StartReceiving(
                this.HandleUpdateAsync,
                this.HandleErrorAsync,
                receiverOptions,
                cancellationToken);

            this.logger.LogInformation("Telegram Bot started receiving messages.");

            return Task.CompletedTask;
        }

        /// <summary>
        /// Stops the service.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Telegram Bot stopping.");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Handles and update.
        /// </summary>
        /// <param name="botClient">Reference to the bot client.</param>
        /// <param name="update">Update.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                if (update.Type == UpdateType.Message && update.Message!.Type == MessageType.Text)
                {
                    var chatId = update.Message.Chat.Id;
                    var messageText = update.Message.Text;

                    logger.LogInformation($"Received a '{messageText}' message in chat {chatId}.");
                }

                if (update.Type == UpdateType.InlineQuery)
                {
                    string messageText = update.InlineQuery!.Query;
                    string id = update.InlineQuery!.Id;
                    logger.LogInformation($"Received inline query with {id} and {messageText}");
                    string result = botTextGenerator.GetSwastika(messageText);
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        InlineQueryResult[] results =
                        {
                            new InlineQueryResultArticle(
                                id: "1",
                                title: "Swaston",
                                new InputTextMessageContent($"<code>{result}</code>")
                                {
                                    ParseMode = ParseMode.Html,
                                })
                            {
                                Description = "Make swaston from words",
                            },
                        };

                        await botClient.AnswerInlineQueryAsync(id, results, cancellationToken: cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(botClient, ex, cancellationToken);
            }
        }

        /// <summary>
        /// Handles an error if any.
        /// </summary>
        /// <param name="botClient">Bot client.</param>
        /// <param name="exception">Exception.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task.</returns>
        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Error received from Telegram Bot");
            return Task.CompletedTask;
        }
    }
}