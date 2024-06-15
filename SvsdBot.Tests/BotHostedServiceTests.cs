using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Types;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace SvsdBot.Tests
{
    [TestClass]
    public class BotHostedServiceTests
    {
        private Mock<IOptions<BotConfiguration>> mockBotConfig;
        private Mock<ILogger<BotHostedService>> mockLogger;
        private Mock<ITelegramBotClient> mockBotClient;
        private BotConfiguration botConfig;

        [TestInitialize]
        public void Setup()
        {
            this.mockBotConfig = new Mock<IOptions<BotConfiguration>>();
            this.mockLogger = new Mock<ILogger<BotHostedService>>();
            this.mockBotClient = new Mock<ITelegramBotClient>();

            this.botConfig = new BotConfiguration { BotToken = "fake_token" };
            this.mockBotConfig.Setup(x => x.Value).Returns(this.botConfig);
        }

        [TestMethod]
        public async Task StartAsync_ShouldStartReceivingMessages()
        {
            // Arrange
            var hostedService = new BotHostedService(mockBotConfig.Object, mockLogger.Object);
            var cancellationToken = new CancellationToken();

            // Act
            await hostedService.StartAsync(cancellationToken);

            // Assert
            mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Telegram Bot started receiving messages.")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        [TestMethod]
        public async Task StopAsync_ShouldLogStoppingMessage()
        {
            // Arrange
            var hostedService = new BotHostedService(mockBotConfig.Object, mockLogger.Object);
            var cancellationToken = new CancellationToken();

            // Act
            await hostedService.StopAsync(cancellationToken);

            // Assert
            mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Telegram Bot stopping.")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        [TestMethod]
        public async Task HandleUpdateAsync_ShouldLogReceivedMessage()
        {
            // Arrange
            var hostedService = new BotHostedService(mockBotConfig.Object, mockLogger.Object);
            var update = new Update
            {
                Message = new Message
                {
                    Chat = new Chat { Id = 123 },
                    Text = "Hello",
                },
            };
            var cancellationToken = new CancellationToken();

            // Act
            await hostedService.StartAsync(cancellationToken);
            await hostedService.HandleUpdateAsync(mockBotClient.Object, update, cancellationToken);

            // Assert
            mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Received a 'Hello' message in chat 123.")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        [TestMethod]
        public async Task HandleErrorAsync_ShouldLogErrorMessage()
        {
            // Arrange
            var hostedService = new BotHostedService(mockBotConfig.Object, mockLogger.Object);
            var exception = new System.Exception("Test exception");
            var cancellationToken = new CancellationToken();

            // Act
            await hostedService.HandleErrorAsync(mockBotClient.Object, exception, cancellationToken);

            // Assert
            mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Error received from Telegram Bot")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }
    }
}