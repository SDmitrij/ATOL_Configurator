using System;
using System.IO;
using System.Text.Json.Serialization;

namespace Configurator.Base.Model
{
    public class Config
    {
        [JsonPropertyName("jsonToApply")]
        public string JsonToApply { get; set; }

        [JsonPropertyName("resultDirectory")]
        public string ResultDirectory { get; set; }

        [JsonPropertyName("needToApplyJson")]
        public bool NeedToApplyJson { get; set; }

        [JsonIgnore]
        public string HardwareStateResultTxt 
        {   
            get { return ResolveResultFilepath("hardware_state.txt"); }
            set { }
        }

        [JsonIgnore]
        public string HardwareStateResultJson
        {
            get { return ResolveResultFilepath("hardware_state.json"); }
            set { }
        }

        [JsonIgnore]
        public string ExecutionResultTxt
        {
            get { return ResolveResultFilepath("execution_result.txt"); }
            set { }
        }

        private string ResolveResultFilepath(string filename)
        {
            if (ResultDirectory is null || ResultDirectory == string.Empty)
                throw new Exception("Can't define file with result, because result directory is not defined, " +
                    "check config file");
            return Path.Combine(ResultDirectory, filename);
        }     
    }
}
