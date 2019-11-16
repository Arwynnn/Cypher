using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cypher
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandService;
        private readonly IServiceProvider _services;

        public CommandHandler(DiscordSocketClient client, CommandService commandService, IServiceProvider services)
        {
            _client = client;
            _commandService = commandService;
            _services = services;
        }

        public async Task InitializeAsync()
        {
            await _commandService.AddModulesAsync(
                Assembly.GetExecutingAssembly(),
                _services);

            HookEvents();
        }

        private void HookEvents()
        {
            _client.MessageReceived += HandleMessageAsync;
            _commandService.Log += LogAsync;
        }

        private Task LogAsync(LogMessage logMessage)
        {
            Console.WriteLine($"{logMessage.Source}: {logMessage.Message}");
            return Task.CompletedTask;
        }

        private async Task HandleMessageAsync(SocketMessage socketMessage)
        {
            if (!(socketMessage is SocketUserMessage socketUserMessage)) { return; }

            var argPos = 0;

            if (!socketUserMessage.HasStringPrefix(ConfigService.Config.Prefix, ref argPos) ||
                socketUserMessage.Author.IsBot) { return; }

            var context = new SocketCommandContext(_client, socketUserMessage);

            await _commandService.ExecuteAsync(
                context,
                argPos,
                _services);

        }
    }
}
