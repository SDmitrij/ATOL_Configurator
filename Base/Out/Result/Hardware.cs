using Configurator.Base.Model;

namespace Configurator.Base.Out.Result
{
    public class Hardware : IResult
    {
        private readonly string _result;
        private readonly bool _json;
        private Config _config;
      
        public Hardware(string result, bool json = false)
        {
            _result = result;
            _json = json;
        }

        public void ApplyConfig(Config config) => _config = config;
        public string GetResult() => _result;

        public string GetResultFile()
        {
            if (_json)
                return _config.HardwareStateResultJson;
            return _config.HardwareStateResultTxt;
        }

        public bool Json() => _json;
    }
}
