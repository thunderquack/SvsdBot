using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SvsdBot.Tests
{
    [TestClass]
    public class HostConfigurationTests
    {
        /// <summary>
        /// Tests that the host is configured correctly.
        /// </summary>
        [TestMethod]
        public async Task Host_ShouldBeConfiguredCorrectly()
        {
            // Arrange
            var args = new string[] { };
            var hostBuilder = Program.CreateHostBuilder(args);

            // Act
            var host = hostBuilder.Build();

            // Assert
            var config = host.Services.GetRequiredService<IConfiguration>();
            var botConfig = config.GetSection("BotConfiguration").Get<BotConfiguration>();
            Assert.IsNotNull(botConfig);
            Assert.IsFalse(string.IsNullOrEmpty(botConfig.BotToken));

            var hostedService = host.Services.GetService<IHostedService>() as BotHostedService;
            Assert.IsNotNull(hostedService);

            var logger = host.Services.GetService<ILogger<BotHostedService>>();
            Assert.IsNotNull(logger);

            await host.StartAsync();
            await host.StopAsync();
        }
    }
}