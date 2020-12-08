using Configurator.Base.Model;
using System.IO;
using System.Text.Json;

namespace Configurator.Base.Initialize
{
    public class ConfigInstance
    {
        private static ConfigInstance _configInstance;
        private readonly Config _config;

        private ConfigInstance(string configFile)
        {
            var json = File.ReadAllText(configFile);
            _config = JsonSerializer.Deserialize<Config>(json);
        }

        public static ConfigInstance GetInstance(string configFile)
        {
            if (_configInstance is null) _configInstance = new ConfigInstance(configFile);
            return _configInstance;
        }

        public Config GetConfig()
        {
            return _config;
        }
    }
}
