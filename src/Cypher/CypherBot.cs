using Cypher.Services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Cypher
{
    public class CypherBot
    {
        private readonly DiscordSocketConfig _discordConfig;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandService;
        private readonly CommandServiceConfig _commandServiceConfig;
        private CommandHandler _commandHandler;
        private IServiceProvider _services;

        public CypherBot()
        {
            _discordConfig = new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug,
                ExclusiveBulkDelete = true,
                AlwaysDownloadUsers = true,
                MessageCacheSize = 50
            };

            _commandServiceConfig = new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                LogLevel = LogSeverity.Info
            };

            _client = new DiscordSocketClient(_discordConfig);
            _commandService = new CommandService(_commandServiceConfig);
        }

        public async Task InitializeAsync()
        {
            _services = ConfigureServices();
            ConfigService.SetupConfig();
            _commandHandler = _services.GetRequiredService<CommandHandler>();

            await _commandHandler.InitializeAsync();
            await _client.LoginAsync(TokenType.Bot, ConfigService.Config.Token);
            await _client.StartAsync();

            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;

            await Task.Delay(-1);
        }

        private async Task ReadyAsync()
        {
            await _client.SetStatusAsync(UserStatus.Idle);
            await _client.SetGameAsync(ConfigService.Config.Status, type: ActivityType.Watching);
        }

        private Task LogAsync(LogMessage logMessage)
        {
            Console.WriteLine($"{logMessage.Source}: {logMessage.Message}");
            return Task.CompletedTask;
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commandService)
                .AddSingleton<CommandHandler>()
                .AddSingleton<RandomService>()
                .BuildServiceProvider();
        }
    }
}
