using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SvsdBot;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class BotHostedService : IHostedService
{
    private readonly ILogger<BotHostedService> logger;
    private readonly BotConfiguration botConfig;
    private ITelegramBotClient botClient;

    public BotHostedService(IOptions<BotConfiguration> botConfig, ILogger<BotHostedService> logger)
    {
        this.botConfig = botConfig.Value;
        this.logger = logger;
    }

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

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        this.logger.LogError(exception, "Error received from Telegram Bot");
        return Task.CompletedTask;
    }
}