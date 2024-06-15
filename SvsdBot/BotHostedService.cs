using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SvsdBot
{
    /// <summary>
    /// Hosted service for the bot.
    /// </summary>
    public class BotHostedService(IOptions<BotConfiguration> botConfig, ILogger<BotHostedService> logger) : IHostedService
    {
        private readonly ILogger<BotHostedService> logger = logger;
        private readonly BotConfiguration botConfig = botConfig.Value;
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
            };

            this.botClient.StartReceiving(
                this.HandleUpdateAsync,
                this.HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );

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

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message!.Type == MessageType.Text)
            {
                var chatId = update.Message.Chat.Id;
                var messageText = update.Message.Text;

                this.logger.LogInformation($"Received a '{messageText}' message in chat {chatId}.");

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "You said:\n" + messageText,
                    cancellationToken: cancellationToken);
            }
        }

        /// <summary>
        /// Handles an error if any.
        /// </summary>
        /// <param name="botClient">Bot client.</param>
        /// <param name="exception">Exception.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task.</returns>
        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            this.logger.LogError(exception, "Error received from Telegram Bot");
            return Task.CompletedTask;
        }
    }
}