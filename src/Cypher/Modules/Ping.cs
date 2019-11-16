using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Cypher.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
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
            if (user is null)
            {
                await ReplyAsync("You need to enter a user.");
                return;
            }

            if (user.Id == Context.Client.CurrentUser.Id)
            {
                await ReplyAsync("Of course I am a bot, you imbecile! 👿");
                return;
            }

            if (user.IsBot)
            {
                await ReplyAsync($"{user.Username} is a good bot :)");
            }
            else
            {
                await ReplyAsync($"{user.Username} is not a good bot >:(");
            }
        }
    }
}
