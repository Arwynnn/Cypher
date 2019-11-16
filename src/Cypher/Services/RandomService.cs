using Discord.WebSocket;

namespace Cypher.Services
{
    public class RandomService
    {
        //Field
        private readonly DiscordSocketClient _client;
        
        //Constructor (ctor)
        public RandomService(DiscordSocketClient client)
        {
            _client = client;
        }

        public string CheckUserIsBot(SocketGuildUser user = null)
        {
            if (user is null)
            {
                return "You need to enter a user.";
            }

            if (user.Id == _client.CurrentUser.Id)
            {
                return "Of course I am a bot, you imbecile! 👿";
             
            }

            if (user.IsBot)
            {
                return $"{user.Username} is a good bot :)";
            }
            else
            {
                return $"{user.Username} is not a good bot >:(";
            }
        }
    }
}
