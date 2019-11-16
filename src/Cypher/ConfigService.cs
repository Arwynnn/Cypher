using System.IO;
using Newtonsoft.Json;

namespace Cypher
{
    public static class ConfigService
    {
        public static CypherConfig Config { get; private set; }

        public static void SetupConfig()
        {
            Config = GetCypherConfig();
        }

        private static CypherConfig GetCypherConfig()
        {
            var data = File.ReadAllText("config.json");
            return JsonConvert.DeserializeObject<CypherConfig>(data);
        }
    }
}
