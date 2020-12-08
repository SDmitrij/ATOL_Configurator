using Configurator.Base.Model;
using Configurator.Base.Out.Result;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Configurator.Base.Out
{
    public class Log
    {
        #region private
        private readonly Config _config;
        private readonly ICollection<IResult> _results;
        #endregion
        public Log(Config config)
        {
            _config = config;
            _results = new List<IResult>();
            CreateInfrastructure();
        }
       
        public void Accept(IResult result)
        {
            result.ApplyConfig(_config);
            _results.Add(result);
        }

        public void Write()
        {
            if (_results.Count() == 0) return;
            foreach(var result in _results)
            {
                WriteToFile(result);
            }
        }
        #region private
        private void WriteToFile(IResult result)
        {
            if (result.Json()) File.WriteAllText(result.GetResultFile(), result.GetResult());
            File.AppendAllText(result.GetResultFile(), result.GetResult());          
        }
     
        private void CreateInfrastructure()
        {
            if (!Directory.Exists(_config.ResultDirectory))
                Directory.CreateDirectory(_config.ResultDirectory);   
        }
        #endregion
    }
}
