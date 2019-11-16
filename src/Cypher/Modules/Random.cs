using Cypher.Services;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Cypher.Modules
{
    public class Random : ModuleBase<SocketCommandContext>
    {
        private readonly RandomService _randomService;

        public Random(RandomService randomService)
        {
            _randomService = randomService;
        }

        [Command("Ping")][Summary("Replies with Pong")]
        public async Task PingCommand()
        {
            await ReplyAsync("Pong");
        }

        [Command("Echo")][Summary("Echoes back the entered string")]
        public async Task EchoCommand([Remainder]string text)
        {
            await ReplyAsync($"You said {text}");
        }

        [Command("Check")]
        public async Task CheckCommand(SocketGuildUser user = null)
        {
            var result = _randomService.CheckUserIsBot(user);
            await ReplyAsync(result);
        }
    }
}
