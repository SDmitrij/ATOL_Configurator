using Configurator.Base.Model;
using System;

namespace Configurator.Base.Out.Result
{
    public class Execution : IResult
    {
        private readonly string _result;
        private Config _config;
       
        public Execution(string result) => _result = result;

        public void ApplyConfig(Config config) => _config = config;
       
        public string GetResult() => string.Format("{0} {1}\n", DateTime.Now.ToString(), _result);

        public string GetResultFile() => _config.ExecutionResultTxt;

        public bool Json() => false;
    }
}
