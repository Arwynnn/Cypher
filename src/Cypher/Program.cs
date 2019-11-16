using System.Threading.Tasks;

namespace Cypher
{
    class Program
    {
        static async Task Main()
        {
            var bot = new CypherBot();
            await bot.InitializeAsync();
        }
    }
}
