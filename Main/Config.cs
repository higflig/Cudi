using System.IO;
using Newtonsoft.Json;

namespace Cudi.Main
{
    public class Config
    {
        public Settings Settings;
        private readonly string filePath = "Resources/config.json";

        public Config()
        {
            char[] seperators = new char[]{'/'};
            string[] path = filePath.Split(seperators);

            if (!Directory.Exists(path[0]))
            {
                Directory.CreateDirectory("Resources");
            }

            if (!File.Exists(filePath))
            {
                Settings = new Settings
                {
                    BotOwnerId = 137143359050350592
                };
                File.WriteAllText(filePath, JsonConvert.SerializeObject(Settings, Formatting.Indented));
            }
            else
            {
                Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(filePath));
            }
        }
    }

    public struct Settings
    {
        public string Token;
        public ulong BotOwnerId;
    }
}